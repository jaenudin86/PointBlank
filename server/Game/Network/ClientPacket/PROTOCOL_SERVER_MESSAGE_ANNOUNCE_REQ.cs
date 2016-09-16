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
using Core.Data.Parsers;
using Core.Model;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_REQ : ReceivePacket
    {
        private int id;

        public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            //ReadH();
            id = ReadD();
            Logger.Info("{0}",id);
            getClient().getPlayer().ChannelId = id;
            ChannelsParser._servers[id].addPlayer(getClient().getPlayer());
            Channel channel  = ChannelsParser._servers[id];
            getClient().getPlayer().setChannel(channel);
        }
        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(getClient().getPlayer().ChannelId));
        }
    }
}
