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
    class PROTOCOL_ROOM_CHANGE_PASSWORD_REQ : ReceivePacket
    {
        public string Password;

        public PROTOCOL_ROOM_CHANGE_PASSWORD_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            Password = ReadS(4);
        }
        public override void RunImpl()
        {
            getClient().getPlayer().getRoom().setPassword(Password);
            getClient().SendPacket(new PROTOCOL_ROOM_CHANGE_PASSWORD_ACK(getClient().getPlayer().getRoom()));
        }
    }
}
