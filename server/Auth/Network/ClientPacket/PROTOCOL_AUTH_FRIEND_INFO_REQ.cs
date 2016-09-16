/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
﻿using Core;
using Core.Database.Tables;
using PointBlank.Manager;
using PointBlank.Network;
using PointBlank.Network.ServerPacket;

namespace PointBlank.Network.ClientPacket
{
    internal class PROTOCOL_AUTH_FRIEND_INFO_REQ : ReceivePacket
    {
        public PROTOCOL_AUTH_FRIEND_INFO_REQ(Auth Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
        }

        public override void RunImpl()
        {
            //getClient().SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_ACK(getClient().getAccount()));
            getClient().SendPacket(new PROTOCOL_BASE_GET_SETTINGS_ACK(getClient().getAccount()));
            getClient().SendPacket(new PROTOCOL_MESSENGER_NOTE_LIST_ACK());
        }
    }
}