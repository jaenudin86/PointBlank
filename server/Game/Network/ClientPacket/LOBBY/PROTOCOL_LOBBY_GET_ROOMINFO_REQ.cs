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
using Core.Model;
using System.Threading;
using Game.Network.ServerPacket.LOBBY;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_LOBBY_GET_ROOMINFO_REQ : ReceivePacket
    {
        private int RoomID;

        public PROTOCOL_LOBBY_GET_ROOMINFO_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            RoomID = ReadC();
        }
        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_LOBBY_GET_ROOMINFO_ACK(getClient().getPlayer().getChannel().getRoomInId(RoomID)));
        }
    }
}
