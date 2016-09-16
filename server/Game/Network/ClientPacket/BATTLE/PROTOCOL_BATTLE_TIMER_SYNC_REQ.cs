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
using Core.Database.Tables;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BATTLE_TIMER_SYNC_REQ : ReceivePacket
    {
        private int timeLost;
        PlayerStats stats;

        public PROTOCOL_BATTLE_TIMER_SYNC_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            timeLost = ReadD();
        }
        public override void RunImpl()
        {
            Room room = getClient().getPlayer().getRoom();
            room.setTimeLost(timeLost);
            Player player = getClient().getPlayer();

            if (room != null)
            {
                if (timeLost < 1 & getClient().getPlayer().getRoom().getBombState() == 0)
                {
                    if (room.getLeader().Equals(player))
                    {
                        if (room.getType() == 2) room.setBlueWinRounds(room.getBlueWinRounds() + 1);
                        if (room.getType() == 2 && room.getBlueWinRounds() != room.getKillsByMask())
                        {
                            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                            {
                                SLOT slot = room.getRoomSlotByPlayer(member);
                                slot.setKillMessage(0);
                                slot.setLastKillMessage(0);
                                slot.setOneTimeKills(0);
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_END_ACK(1, 2, getClient().getPlayer().getRoom()));
                            }
                            Thread.Sleep(10000);
                            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                            {
                                member.getClient().SendPacket(new opcode_3865_ACK());
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_START(member.getRoom()));
                            }
                        }
                        else
                        {
                            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                            {
                                SLOT slot = room.getRoomSlotByPlayer(member);
                                if (slot.getState() == SLOT_STATE.SLOT_STATE_BATTLE || slot.getState() == SLOT_STATE.SLOT_STATE_BATTLE_READY || slot.getState() == SLOT_STATE.SLOT_STATE_PRESTART || slot.getState() == SLOT_STATE.SLOT_STATE_LOAD || slot.getState() == SLOT_STATE.SLOT_STATE_RENDEZVOUS)
                                {
                                    slot.setState(SLOT_STATE.SLOT_STATE_NORMAL);

                                    /* Подсчет статистики */
                                    stats = PlayersStatsTable.statistics[member.getClient().getPlayer().AccountID];

                                    stats.setSeasonKills(stats.getSeasonKills() + slot.getAllKills());//подсчет убийств
                                    stats.setSeasonDeaths(stats.getSeasonDeaths() + slot.getAllDeath());//подсчет смертей
                                    stats.setHeadshots(stats.getHeadshots() + slot.getHeadshots());//подсчет попаданий в голову
                                    PlayersStatsTable.UpdateStats(member.getClient().getPlayer().PlayerID, stats.getSeasonKills(), stats.getHeadshots(), stats.getSeasonDeaths());

                                    member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(room));
                                    member.getClient().SendPacket(new PROTOCOL_BATTLE_END_ACK(member, getClient().getPlayer().getRoom()));
                                    member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(room));
                                }
                            }
                            for (int i = 0; i < 16; i++)
                            {
                                SLOT slot = room.getRoomSlot(i);
                                slot.setKillMessage(0);
                                slot.setLastKillMessage(0);
                                slot.setOneTimeKills(0);
                                slot.setAllKills(0);
                                slot.setAllDeahts(0);
                                slot.setBotScore(0);
                            }
                            room.setRedKills(0);
                            room.setRedDeaths(0);
                            room.setBlueKills(0);
                            room.setBlueDeaths(0);
                            room.setFigth(0);
                        }

                    }
                }
            }
        }
    }
}
