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

namespace PointBlank.Network.ServerPacket
{
    class PROTOCOL_LOGIN_CHANNEL_LIST : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0x801);
            WriteD(5404);
            WriteB(new byte[] { 1, 0, 0, 127 });
            WriteH(29890);
            WriteH(32759);
            for (int i = 0; i < 10; i++)
            {
                WriteC(1);
            }
            WriteC(1);
            WriteD(GameServersParser._servers.Count);
            for (int i = 0; i < GameServersParser._servers.Count; i++)
            {
                WriteD(1);
                WriteB(IPAddress.Parse(GameServersParser._servers[i].Ip).GetAddressBytes());
                WriteH((short)GameServersParser._servers[i].Port);
                WriteC((byte)GameServersParser._servers[i].Type);
                WriteH((short)GameServersParser._servers[i].MaxPlayers);
                WriteD(0);
            }
        }
    }
}
