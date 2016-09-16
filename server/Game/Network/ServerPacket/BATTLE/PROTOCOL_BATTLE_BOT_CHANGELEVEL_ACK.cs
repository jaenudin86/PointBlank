/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_BATTLE_BOT_CHANGELEVEL_ACK : SendPacket
    {
        public int AILevel;

        public PROTOCOL_BATTLE_BOT_CHANGELEVEL_ACK(int AILevel)
        {
            this.AILevel = AILevel;
        }

        public override void WriteImpl()
        {
            WriteH(0xD31);
            WriteC((byte)AILevel <= 10 ? (byte)AILevel : (byte)10);
            for (int i = 0; i < 8; i++)
            {
                WriteD(1);// ранг бота?
                WriteD(1);// ранг бота?
            }
        }
    }
}