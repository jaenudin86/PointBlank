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
using Core.Data.Parsers;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_LOBBY_LEAVE_REQ : ReceivePacket
    {
        public PROTOCOL_LOBBY_LEAVE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            
        }
        public override void RunImpl()
        {
            if (getClient() == null)
                return;
            Player player = getClient().getPlayer();
            Channel channel = getClient().getPlayer().getChannel();
            if (channel != null && player != null)
                channel.removePlayer(player);
            getClient().SendPacket(new PROTOCOL_LOBBY_LEAVE_ACK());
        }
    }
}
