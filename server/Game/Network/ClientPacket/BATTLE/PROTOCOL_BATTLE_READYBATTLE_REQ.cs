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
using Core.Data.Parsers;
using Game.Managers;
using Game.Network.ServerPacket;
using Core.Database.Tables;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BATTLE_READYBATTLE_REQ : ReceivePacket
    {
        public PROTOCOL_BATTLE_READYBATTLE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            //Logger.Info("{0}", ReadC());
        }
        public override void RunImpl()
        {
            Channel channel = getClient().getPlayer().getChannel();
            Room room = getClient().getPlayer().getRoom();
            Player player = getClient().getPlayer();

            if (room.getLeader().Equals(getClient().getPlayer()))
            {
                if (room.isFigth() == 0)
                {
                    room.setFigth(1);
                    room.setTimeLost(room.getKillTime() * 60);
                }
                if (room.getPlayers() != null)
                {
                    if (getClient().getPlayer().getRoom().getPlayers() != null)
                    {
                        foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                        {
                            SLOT slot = room.getRoomSlotByPlayer(member);
                            if (slot.getState() == SLOT_STATE.SLOT_STATE_READY && !room.getLeader().Equals(member))
                            {
                                slot.setState(SLOT_STATE.SLOT_STATE_LOAD);
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(room, getClient().getPlayer()));
                            }
                            else if (room.getLeader().Equals(member))
                            {
                                slot.setState(SLOT_STATE.SLOT_STATE_LOAD);
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(room, member));
                            }
                        }
                    }
                }
            }
            else
            {
                SLOT slotLeader = room.getRoomSlotByPlayer(room.getLeader());
                SLOT slot = room.getRoomSlotByPlayer(getClient().getPlayer());
                if (slotLeader.getState() == SLOT_STATE.SLOT_STATE_LOAD || slotLeader.getState() == SLOT_STATE.SLOT_STATE_RENDEZVOUS || slotLeader.getState() == SLOT_STATE.SLOT_STATE_PRESTART || slotLeader.getState() == SLOT_STATE.SLOT_STATE_BATTLE_READY || slotLeader.getState() == SLOT_STATE.SLOT_STATE_BATTLE)
                {
                    slot.setState(SLOT_STATE.SLOT_STATE_LOAD);
                    getClient().SendPacket(new PROTOCOL_BATTLE_READYBATTLE_ACK(room, getClient().getPlayer()));
                }
                if (slot.getState() == SLOT_STATE.SLOT_STATE_READY)
                {
                    slot.setState(SLOT_STATE.SLOT_STATE_NORMAL);
                }
                else if (slot.getState() == SLOT_STATE.SLOT_STATE_NORMAL)
                {
                    slot.setState(SLOT_STATE.SLOT_STATE_READY);
                }
            }
            // слать пакет ROOM_INFO всем игрокам
            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(room));
            }
        }
    }
}
