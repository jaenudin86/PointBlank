/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Core.Data.Parsers;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_GAME_CHANNEL_LIST : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0x801);
            WriteD(5404);
            WriteB(new byte[] { 1, 0, 0, 127 });
            WriteH(29890);
            WriteH(32759);
            for (int i = 0; i < ChannelsParser._servers.Count; i++)
            {
                WriteC((byte)ChannelsParser._servers[i].getType());
            }
            WriteC(1);
            WriteD(2);
            for (int i = 0; i < 2; i++)
            {
                WriteD(1);
                WriteB(IPAddress.Parse("127.0.0.1").GetAddressBytes());
                int port = 39190;
                WriteH((short)port);
                WriteC((byte)6);
                WriteH((short)300);
                WriteD(0);
            }
        }
    }
}
