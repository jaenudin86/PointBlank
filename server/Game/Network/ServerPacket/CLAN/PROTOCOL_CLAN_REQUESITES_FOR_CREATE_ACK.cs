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
    class PROTOCOL_CLAN_REQUESITES_FOR_CREATE_ACK : SendPacket
    {
        public PROTOCOL_CLAN_REQUESITES_FOR_CREATE_ACK()
        {
        }

        public override void WriteImpl()
        {
            WriteH(0x589);
            WriteC(53);//Необходимый ранг
            WriteD(15000);//Необходимое кол-во средств
        }
    }
}
