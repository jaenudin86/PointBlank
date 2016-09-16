/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network;
using Core.Model;
using Game.Network.ServerPacket;

namespace Game.Managers
{
    public class RoomManager
    {
        public static Dictionary<int, List<GameNetwork>> players = new Dictionary<int, List<GameNetwork>>();

        public static void AddPlayer(int RoomID, GameNetwork client)
        {
            if (!players.ContainsKey(RoomID))
            {
                players.Add(RoomID, new List<GameNetwork>());
            }
            players[RoomID].Add(client);
        }

        public static GameNetwork getPlayer(int RoomID, ulong PlayerID)
        {
            foreach (GameNetwork im in players[RoomID].ToArray())
            {
                if (im.getPlayer().PlayerID == PlayerID)
                {
                    return im;
                }
                else
                {
                    Logger.Info("Error");
                }
            }
            return null;
        }

        public static List<GameNetwork> getPlayers(int RoomID)
        {
            return players[RoomID];
        }


        /*public static void ChangeRoomState(ROOM_STATE RoomState, Player p, Room room)
        {
            if (room.getState() == ROOM_STATE.ROOM_STATE_PRE_BATTLE)
            {
                if (p.PlayerID == room.getLeader().PlayerID)
                {
                    // фраги
                }

                for (int slot = 0; slot < 15; ++slot)
                {
                    Player playerBySlot = room.getPlayerBySlot(slot);
                    GameNetwork playerFromPlayerId2 = RoomManager.getPlayer(playerBySlot.getRoom().getRoomId(), playerBySlot.PlayerID);
                    if (room.Slots[slot].State == SLOT_STATE.SLOT_STATE_BATTLE_READY && room.Slots[slot].PlayerID > 0 && playerBySlot != null)
                    {
                        room.СhangeSlotState(slot, SLOT_STATE.SLOT_STATE_BATTLE);
                        // GameNetwork playerFromPlayerId2 = RoomManager.getPlayer(playerBySlot.getRoom().getRoomId(), playerBySlot.PlayerID);
                        playerFromPlayerId2.SendPacket(new opcode_3879_ACK(playerBySlot.getRoom()));
                        playerFromPlayerId2.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
                        if (p.PlayerID != playerBySlot.PlayerID)
                        {
                            playerFromPlayerId2.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
                        }
                        playerFromPlayerId2.SendPacket(new opcode_3865_ACK());
                        playerFromPlayerId2.SendPacket(new opcode_3867_ACK());
                    }
                }
                room.setState(ROOM_STATE.ROOM_STATE_BATTLE);
            }
            else
            {
                GameNetwork player = RoomManager.getPlayer(p.getRoom().getRoomId(), p.PlayerID);
                room.СhangeSlotState(p.getSlot(), SLOT_STATE.SLOT_STATE_BATTLE);
                player.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK()); // TODO: Player
                for (int slot = 0; slot < 15; ++slot)
                {
                    Player playerBySlot = room.getPlayerBySlot(slot);
                    GameNetwork playerFromPlayerId2 = RoomManager.getPlayer(playerBySlot.getRoom().getRoomId(), playerBySlot.PlayerID);
                    if (room.Slots[slot].State == SLOT_STATE.SLOT_STATE_BATTLE_READY && room.Slots[slot].PlayerID > 0)
                    {
                        if (p.PlayerID != playerBySlot.PlayerID)
                        {
                            playerFromPlayerId2.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
                        }
                        player.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
                    }

                }
                player.SendPacket(new opcode_3865_ACK());
                player.SendPacket(new opcode_3867_ACK());
            }
        }*/

        public static void RoomTask(int channel, Room room)
        {
            int countPlayerPrestart = 0;

            foreach(GameNetwork player in getPlayers(room.getId()).ToArray()) 
            {
                SLOT slot = room.ROOM_SLOT[player.getPlayer().getSlot()];
                if ((int)slot.getState() > 8 && (int)slot.getState() < 12)
                {
                    countPlayerPrestart++;
                }
            }
            SLOT leader = room.ROOM_SLOT[room.getLeader().getSlot()];
            if ((int)leader.getState() < 13)
            {
                if (countPlayerPrestart == 0)
                {
                    if ((int)leader.getState() < 13)
                    {
                        foreach (GameNetwork member in getPlayers(room.getId()).ToArray())
                        {
                            SLOT playerSlot = room.ROOM_SLOT[member.getPlayer().getSlot()];
                            if (playerSlot.getState() == SLOT_STATE.SLOT_STATE_BATTLE_READY)
                            {
                                playerSlot.setState(SLOT_STATE.SLOT_STATE_BATTLE);
                                member.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK(room, member.getPlayer()));
                                foreach (GameNetwork toSend in getPlayers(room.getId()).ToArray())
                                {
                                    if (!toSend.Equals(member))
                                    {
                                        SLOT toSendplayerSlot = room.ROOM_SLOT[toSend.getPlayer().getSlot()];
                                        if ((int)toSendplayerSlot.getState() > 11)
                                        {
                                            member.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK(room, toSend.getPlayer()));
                                        }
                                    }
                                }
                                member.SendPacket(new opcode_3865_ACK());
                                member.SendPacket(new PROTOCOL_BATTLE_ROUND_START(room));
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (GameNetwork player in getPlayers(room.getId()).ToArray()) 
                {
                    SLOT playerSlot = room.ROOM_SLOT[player.getPlayer().getSlot()];
                    if ((int)playerSlot.getState() == 12)
                    {
                        player.SendPacket(new opcode_3890_ACK(room));
                        playerSlot.setState(SLOT_STATE.SLOT_STATE_BATTLE);
                        player.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK(room, player.getPlayer()));
                        foreach (GameNetwork player1 in getPlayers(room.getId()).ToArray()) 
                        {
                            SLOT why = room.ROOM_SLOT[player1.getPlayer().getSlot()];
                            if ((int)why.getState() == 13 && !player.Equals(player1))
                            {
                                player1.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK(room, player.getPlayer()));
                                player.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK(room, player1.getPlayer()));
                            }
                        }
                        player.SendPacket(new opcode_3865_ACK());
                        player.SendPacket(new PROTOCOL_BATTLE_ROUND_START(room));
                    }

                }
            }
        }
    }
}
