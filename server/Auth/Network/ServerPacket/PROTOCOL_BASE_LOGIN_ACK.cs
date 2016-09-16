/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointBlank.Network.ServerPacket
{
    class PROTOCOL_BASE_LOGIN_ACK : SendPacket
    {
        private long _result;

        public PROTOCOL_BASE_LOGIN_ACK(long result)
        {
            _result = result;
        }

        public override void WriteImpl()
        {
            WriteH(2564);
            WriteQ(_result); ;
            WriteB(new byte[4]
            {
                (byte) 189,
                (byte) 197,
                (byte) 19,
                (byte) 0
            });
            WriteQ(0L);
        }
    }
}
