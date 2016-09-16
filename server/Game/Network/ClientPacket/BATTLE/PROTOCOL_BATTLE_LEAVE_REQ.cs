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
using Core.Database.Tables;
using Game.Network.ServerPacket;
using Core.Managers;

namespace Game.Network.ClientPacket
{
    public class PROTOCOL_BATTLE_LEAVE_REQ : ReceivePacket
    {
        private int coupon;
        PlayerStats stats;

        public PROTOCOL_BATTLE_LEAVE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            coupon = ReadD();//купон на свободный выход
        }

        public override void RunImpl()
        {
            getClient().SendPacket(new PACKET_LEVEL_UP_ACK(getClient().getPlayer().getRank()));

            Player player = getClient().getPlayer();
            Room room = player.getRoom();
            stats = PlayersStatsTable.statistics[player.PlayerID];
            room.getRoomSlotByPlayer(player).setState(SLOT_STATE.SLOT_STATE_NORMAL);

            if (coupon == 0)
            {
                PlayersStatsTable.UpdateEscapes(player.PlayerID, stats.getSeasonEscapes() + 1);//записываем побег
            }

            getClient().getPlayer().getRoom().getRoomSlotByPlayer(getClient().getPlayer()).setState(SLOT_STATE.SLOT_STATE_NORMAL); // статус слота

            if (player == room.getLeader() & room.getPlayers().Count == 1)//если игрок лидер и он один в комнате
            {
                foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                {
                    SLOT slot = room.getRoomSlotByPlayer(member);
                    if (slot.getState() == SLOT_STATE.SLOT_STATE_BATTLE || slot.getState() == SLOT_STATE.SLOT_STATE_BATTLE_READY || slot.getState() == SLOT_STATE.SLOT_STATE_PRESTART || slot.getState() == SLOT_STATE.SLOT_STATE_LOAD || slot.getState() == SLOT_STATE.SLOT_STATE_RENDEZVOUS)
                    {
                        member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(room));//отправляем инфу о комнате
                        member.getClient().SendPacket(new PROTOCOL_BATTLE_LEAVE_ACK(room.getRoomSlotByPlayer(player).getId()));//инфа о том,что игрок вышел из боя
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

                player.getClient().SendPacket(new PROTOCOL_BATTLE_END_ACK(player, room));
            }

            if(player != room.getLeader())//если игрок не лидер
            {
                foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                {
                    SLOT slot = room.getRoomSlotByPlayer(member);
                    if (slot.getState() == SLOT_STATE.SLOT_STATE_BATTLE || slot.getState() == SLOT_STATE.SLOT_STATE_BATTLE_READY || slot.getState() == SLOT_STATE.SLOT_STATE_PRESTART || slot.getState() == SLOT_STATE.SLOT_STATE_LOAD || slot.getState() == SLOT_STATE.SLOT_STATE_RENDEZVOUS)
                    {
                        member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(room));//отправляем инфу о комнате
                        member.getClient().SendPacket(new PROTOCOL_BATTLE_LEAVE_ACK(room.getRoomSlotByPlayer(player).getId()));//инфа о том,что игрок вышел из боя
                    }
                }
            }

            if(player == room.getLeader() & room.getPlayers().Count > 1)//если игрок лидер и игроков в комнате больше одного
            {
                room.setNewLeader();//выбираем нового лидера
                BattleHandler.ChangeHost(room, room.getLeader());//отправляем на боевой сервер информацию о новом лидере комнаты
                foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                {
                    SLOT slot = room.getRoomSlotByPlayer(member);
                    if (slot.getState() == SLOT_STATE.SLOT_STATE_BATTLE || slot.getState() == SLOT_STATE.SLOT_STATE_BATTLE_READY || slot.getState() == SLOT_STATE.SLOT_STATE_PRESTART || slot.getState() == SLOT_STATE.SLOT_STATE_LOAD || slot.getState() == SLOT_STATE.SLOT_STATE_RENDEZVOUS)
                    {
                        member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(room));//отправляем инфу о комнате
                        member.getClient().SendPacket(new PROTOCOL_BATTLE_LEAVE_ACK(room.getRoomSlotByPlayer(player).getId()));//инфа о том,что игрок вышел из боя
                        member.getClient().SendPacket(new PROTOCOL_BATTLE_CHANGE_NETWORK_ACK(room));//отправляем пакет с инфой о смене адреса
                    }
                }
            }


        }
    }
}
