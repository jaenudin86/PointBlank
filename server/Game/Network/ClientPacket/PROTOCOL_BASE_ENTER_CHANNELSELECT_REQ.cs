/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network.ServerPacket;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BASE_ENTER_CHANNELSELECT_REQ : ReceivePacket
    {
        public PROTOCOL_BASE_ENTER_CHANNELSELECT_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            Logger.Info(ReadS(4));
        }
        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_BASE_ENTER_CHANNELSELECT_ACK());
        }
    }
}
