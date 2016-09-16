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
    class PROTOCOL_CLAN_CREATE_ACK : SendPacket
    {
        private string Info;
        private string Name;
        private Player player;

        public PROTOCOL_CLAN_CREATE_ACK(string Name)
        {
            this.Name = Name;
        }

        public override void WriteImpl()
        {
            WriteH(0x51F);
            WriteD(0);
            WriteC((byte)96);
            WriteC((byte)65);
            WriteH((short)0);
            WriteS(Name, 16);
            WriteH((short)0);
            WriteC(1);
            WriteB(new byte[5] { (byte)50, (byte)75, (byte)5, (byte)51, (byte)1 });
            WriteB(new byte[14]);
            WriteC((byte)2);
            WriteC((byte)23);
            WriteC((byte)6);
            WriteB(new byte[5]);
            WriteS("Admin", 33);
            WriteC(53);
            WriteS("", 120);
            WriteB(new byte[12]);
            WriteD(4859);
            WriteD(4860);
        }
    }
}
