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
    class PROTOCOL_SHOP_LIST_REQ : ReceivePacket
    {
        private int error;

        public PROTOCOL_SHOP_LIST_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            error = ReadD(); // error ?
        }
        public override void RunImpl()
        {
            if (error == 0)
            {
                getClient().SendPacket(new PROTOCOL_SHOP_GET_ITEMS_ACK(getGoods()));
                getClient().SendPacket(new PROTOCOL_SHOP_GET_GOODS_ACK(getGoods()));
                getClient().SendPacket(new PROTOCOL_SHOP_TEST_1_ACK());
                getClient().SendPacket(new PROTOCOL_SHOP_TEST_2_ACK());
                getClient().SendPacket(new PROTOCOL_SHOP_GET_MATCHING_ACK(getGoods()));
            }
        }

        public List<Good> getGoods()
        {
            List<Good> collection = new List<Good>();
            foreach (Good item in GoodsParser.goods.Values)
            {
                collection.Add(item);
            }
            return collection;
        }
    }
}
