/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PointBlank.Network.ServerPacket;
using Core.Database.Tables;

namespace PointBlank.Network.ClientPacket
{
    class PROTOCOL_BASE_GET_MYINFO_REQ : ReceivePacket
    {
        public PROTOCOL_BASE_GET_MYINFO_REQ(Auth Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            
        }
        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_BASE_GET_MYINFO_ACK(getClient().getAccount()));
        }
    }
}
