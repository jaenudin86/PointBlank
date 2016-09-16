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
    class PROTOCOL_LOBBY_GET_ROOMLIST_ACK : SendPacket
    {
        private Channel channel;
        public PROTOCOL_LOBBY_GET_ROOMLIST_ACK(Channel channel)
        {
            this.channel = channel;
        }
        public override void WriteImpl()
        {
            WriteH(0xC02);
            WriteD(channel.getRooms().Count);
            //
            WriteD(0);
            WriteD(channel.getRooms().Count);
            foreach (Room room in channel.getRooms().Values)
            {
                WriteD(room.getId());
                WriteS(room.getName(), 23);
                WriteC((byte)room.getMapId());
                WriteC(0);
                WriteC(0);
                WriteC((byte)room.getType());
                WriteC((byte)room.isFigth());
                WriteC((byte)room.getPlayers().Count);
                WriteC((byte)room.getSlots());
                WriteC(1);
                WriteC(0x2b);
                WriteC((byte)room.getPassword().Length);
                WriteC((byte)room.getSpecial());
            }
            //
            WriteD(channel.getPlayers().Count);
            WriteD(0);
            WriteD(channel.getPlayers().Count);
            int i = 1;//0
            foreach (Player player in channel.getPlayers().Values)
            {
                i++;
                if (player != null)
                {
                    WriteD(i);
                    WriteC(Convert.ToByte(player == null || player.getClan() == null ? (int)byte.MaxValue : player.getClan().getLogo1()));
                    WriteC(Convert.ToByte(player == null || player.getClan() == null ? (int)byte.MaxValue : player.getClan().getLogo2()));
                    WriteC(Convert.ToByte(player == null || player.getClan() == null ? (int)byte.MaxValue : player.getClan().getLogo3()));
                    WriteC(Convert.ToByte(player == null || player.getClan() == null ? (int)byte.MaxValue : player.getClan().getLogo4()));
                    WriteS(Convert.ToString(player == null || player.getClan() == null ? "" : player.getClan().getName()), 17);
                    WriteH(Convert.ToSByte(player.Rank));
                    WriteS(Convert.ToString(player.PlayerName), 33);
                    WriteC(0); // Цвет?
                    WriteC(0); // Цвет?
                }
                else
                {
                    WriteD(i);
                    WriteC(0);
                    WriteC(0);
                    WriteC(0);
                    WriteC(0);
                    WriteS("", 16);
                    WriteH(0);
                    WriteS("", 33);
                    WriteC(0); // Цвет?
                    WriteC(0); // Цвет?
                }
            }
            
           
       
        }
    }
}
