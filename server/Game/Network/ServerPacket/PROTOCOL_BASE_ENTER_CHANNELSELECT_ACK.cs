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
    class PROTOCOL_BASE_ENTER_CHANNELSELECT_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0xA0C);
            WriteD(10);
            WriteD(Channel.MAX_PLAYERS_COUNT);
            for (int i = 0; i < 10; i++)
            {
                WriteD((byte)ChannelsParser._servers[i].getPlayers().Count);
            }
        }
    }
}