/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Model;
using Game.Network.ServerPacket;
using System.Threading;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BATTLE_BOMB_UNTAB_REQ : ReceivePacket
    {
        public int slot;

        public PROTOCOL_BATTLE_BOMB_UNTAB_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            slot = ReadD();
        }

        public override void RunImpl()
        {
            Room room = getClient().getPlayer().getRoom();
                getClient().getPlayer().getRoom().setBombState(0);
                room.setRedKills(0);
                room.setBlueKills(0);
                room.setBlueWinRounds(room.getBlueWinRounds() + 1);
                if (room.getBlueWinRounds() == room.getKillsByMask())
                {
                    foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                    {
                        SLOT slot = room.getRoomSlotByPlayer(member);
                        slot.setKillMessage(0);
                        slot.setLastKillMessage(0);
                        slot.setOneTimeKills(0);
                        slot.setAllKills(0);
                        slot.setAllDeahts(0);
                        member.getClient().SendPacket(new PROTOCOL_BATTLE_END_ACK(member, room));
                        room.setRedKills(0);
                        room.setRedDeaths(0);
                        room.setBlueKills(0);
                        room.setBlueDeaths(0);
                        room.setFigth(0);
                        room.setBlueWinRounds(0);
                        room.setRedWinRounds(0);
                    }
                }
                else
                {
                    foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                    {
                        SLOT playerSlot = room.getRoomSlotByPlayer(member);
                        playerSlot.setKillMessage(0);
                        playerSlot.setLastKillMessage(0);
                        playerSlot.setOneTimeKills(0);
                        member.getClient().SendPacket(new PROTOCOL_BATTLE_BOMB_UNTAB_ACK(slot));
                        if (room.getType() == 2)
                        {
                            member.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_END_ACK(1, 3, getClient().getPlayer().getRoom()));
                        }
                    }
                    if (room.getType() == 2)
                    {
                        Thread.Sleep(10000);
                        foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                        {
                            member.getClient().SendPacket(new opcode_3865_ACK());
                            member.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_START(member.getRoom()));
                        }

                    }
                }
            }

        }
    }
