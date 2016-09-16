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
using Core.Data.Parsers;
using Game.Network.ServerPacket;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_LOBBY_GET_ROOMLIST_REQ : ReceivePacket
    {
        public PROTOCOL_LOBBY_GET_ROOMLIST_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            
        }
        public override void RunImpl()
        {
            Channel channel = ChannelsParser._servers[getClient().getPlayer().getChannel().getId()];
            Room room = getClient().getPlayer().getRoom();
            if (channel != null && room != null)
                if (room.getPlayers() == null)
                    channel.removeRoom(room);

            getClient().SendPacket(new PROTOCOL_LOBBY_GET_ROOMLIST_ACK(getClient().getPlayer().getChannel()));
            Logger.Info(getClient().getPlayer().PlayerName);
        }
    }
}
