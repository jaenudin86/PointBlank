/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data.Parsers;
using Core.Model;

namespace Game.Network.ServerPacket
{
    public class PROTOCOL_ROOM_LOBBY_USER_LIST_ACK : SendPacket
    {
        private Channel channel;

        public PROTOCOL_ROOM_LOBBY_USER_LIST_ACK(Channel channel)
        {
            this.channel = channel;
        }

        public override void WriteImpl()
        {
            int count = channel.getPlayers().Count;
            int index = 0;

            WriteH(3855);
            WriteD(count);

            if(count != 0)
            {
                foreach(var player in channel.getPlayers().ToArray())
                {
                    WriteD((int)index);
                    WriteD(player.Value.getRank());
                    WriteC((byte)33);
                    WriteS(player.Value.getName(), 33);
                    index = index + 1;
                }
            }
        }
    }
}
