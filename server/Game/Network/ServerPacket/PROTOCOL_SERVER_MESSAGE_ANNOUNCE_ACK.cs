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

namespace Game.Network.ServerPacket
{
    class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK : SendPacket
    {
        private int id;
        public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(int id)
        {
            this.id = id;
        }
        public override void WriteImpl()
        {
            String announce = ChannelsParser._servers[id].getAnnounce();
            WriteH(0xA0E);
            WriteD(1);
            WriteH((short)announce.Length);
            WriteS(announce, announce.Length);
            WriteD(0);
        }
    }
}
