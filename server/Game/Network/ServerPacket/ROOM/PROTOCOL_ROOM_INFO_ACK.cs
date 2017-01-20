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

namespace Game.Network.ServerPacket
{
    public class PROTOCOL_ROOM_INFO_ACK : SendPacket
    {
        private Room room;
        public PROTOCOL_ROOM_INFO_ACK(Room room)
        {
            this.room = room;
        }
        public override void WriteImpl()
        {
            try
            {
                if (room != null)
                {
                    WriteH(3861);
                    if (room.getRoomSlotByPlayer(room.getLeader()) == null)
                        room.setNewLeader();

                    WriteD(room.getRoomSlotByPlayer(room.getLeader()).getId());
                    for (int slotId = 0; slotId < 16; ++slotId)
                    {
                        Player player = room.getRoomSlot(slotId).getPlayer();
                        SLOT roomSlot = room.getRoomSlot(slotId);

                        if (player != null)
                        {
                            WriteC((byte)roomSlot.getState());
                            WriteC((byte)roomSlot.getPlayer().getRank());
                            WriteB(new byte[9]);
                            WriteC(roomSlot.getPlayer() == null || roomSlot.getPlayer().ClanID == 0 ? (byte)255 : (byte)roomSlot.getPlayer().Clan.Logo1);
                            WriteC(roomSlot.getPlayer() == null || roomSlot.getPlayer().ClanID == 0 ? (byte)255 : (byte)roomSlot.getPlayer().Clan.Logo2);
                            WriteC(roomSlot.getPlayer() == null || roomSlot.getPlayer().ClanID == 0 ? (byte)255 : (byte)roomSlot.getPlayer().Clan.Logo3);
                            WriteC(roomSlot.getPlayer() == null || roomSlot.getPlayer().ClanID == 0 ? (byte)255 : (byte)roomSlot.getPlayer().Clan.Logo4);
                            WriteC(roomSlot.getPlayer() == null || roomSlot.getPlayer().ClanID == 0 ? (byte)255 : (byte)roomSlot.getPlayer().Clan.Color);
                            WriteD(roomSlot.getPlayer() == null || roomSlot.getPlayer().ClanID == 0 ? 0 : roomSlot.getPlayer().getPCCafe());//пк_кафе
                            WriteH(roomSlot.getPlayer() == null || roomSlot.getPlayer().ClanID == 0 ? (short)0 : (short)roomSlot.getPlayer().getEmblem());//Лычка
                            WriteS(roomSlot.getPlayer() == null || roomSlot.getPlayer().ClanID == 0 ? "" : roomSlot.getPlayer().Clan.Name, Clan.CLAN_NAME_SIZE);
                            WriteC((byte)roomSlot.getPlayer().getEffect1());
                            WriteC((byte)roomSlot.getPlayer().getEffect2());
                            WriteC((byte)roomSlot.getPlayer().getEffect3());
                            WriteC((byte)roomSlot.getPlayer().getEffect4());
                            WriteC((byte)roomSlot.getPlayer().getEffect5());
                            WriteC(0);
                            WriteC(0);
                        }
                        else
                        {
                            WriteC((byte)roomSlot.getState());
                            WriteC(0);
                            WriteB(new byte[9]);
                            WriteC(0xff);
                            WriteC(0xff);
                            WriteC(0xff);
                            WriteC(0xff);
                            WriteC(0);
                            WriteB(new byte[6]);
                            WriteS("", 0x11);
                            WriteH(0);
                            WriteC(0);
                            WriteC(0);

                        }
                    }
                }
                else
                {
                    WriteH((short)3861);
                    WriteD(0);
                    WriteB(new byte[10]
                    {
                      (byte) 1,
                      (byte) 2,
                      (byte) 3,
                      (byte) 4,
                      (byte) 5,
                      (byte) 6,
                      (byte) 7,
                      (byte) 8,
                      (byte) 9,
                      (byte) 10
                    });
                }
            }
            catch(Exception e)
            {
                Logger.Error(e.ToString());
            }
        }
    }
}
