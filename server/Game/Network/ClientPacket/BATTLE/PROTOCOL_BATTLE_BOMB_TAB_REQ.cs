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
    class PROTOCOL_BATTLE_BOMB_TAB_REQ : ReceivePacket
    {
        public int zone;
	    public int slot;
	    public int x, y, z;
        public int RedRounds, BlueRounds;

        public PROTOCOL_BATTLE_BOMB_TAB_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            slot = ReadD();
            zone = ReadC();
            x = ReadD();
            y = ReadD();
            z = ReadD();
        }
        public override void RunImpl()
        {
            Room room = getClient().getPlayer().getRoom();
            getClient().getPlayer().getRoom().setBombState(1);
            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_BATTLE_BOMB_TAB_ACK(zone, slot, x, y, z));
              //  member.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_END_ACK(1));
              //  member.getClient().SendPacket(new PROTOCOL_BATTLE_ROOMINFO_ACK(getClient().getPlayer().getRoom()));
            }
            if (room.getType() == 2)
            {

                RedRounds = getClient().getPlayer().getRoom().getRedWinRounds();
                BlueRounds = getClient().getPlayer().getRoom().getBlueWinRounds();

                Thread.Sleep(42500);
                if (RedRounds == getClient().getPlayer().getRoom().getRedWinRounds() & BlueRounds == getClient().getPlayer().getRoom().getBlueWinRounds())
                {
                    if (getClient().getPlayer().getRoom().getBombState() == 1)
                    {
                        room.setRedWinRounds(room.getRedWinRounds() + 1);
                        room.setBombState(0);
                        room.setRedKills(0);
                        room.setBlueKills(0);
                        if (room.getRedWinRounds() == room.getKillsByMask())
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
                            }
                            room.setRedKills(0);
                            room.setRedDeaths(0);
                            room.setBlueKills(0);
                            room.setBlueDeaths(0);
                            room.setFigth(0);
                            room.setBlueWinRounds(0);
                            room.setRedWinRounds(0);
                        }
                        else
                        {
                            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                            {
                                SLOT slot = room.getRoomSlotByPlayer(member);
                                slot.setKillMessage(0);
                                slot.setLastKillMessage(0);
                                slot.setOneTimeKills(0);
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_END_ACK(0, 2, getClient().getPlayer().getRoom()));
                            }
                            Thread.Sleep(10000);
                            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                            {
                                member.getClient().SendPacket(new opcode_3865_ACK());
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_START(member.getRoom()));
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
