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
using Core.Database.Tables;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_SHOP_BUY_ITEM_REQ : ReceivePacket
    {
        private int id;
        public PROTOCOL_SHOP_BUY_ITEM_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            ReadC();
            id = ReadD(); // error ?
        }
        public override void RunImpl()
        {
            Good good = getGoodItemById(id);
            if (good == null || getClient().getPlayer().getGp() < good.getPricePoints() || getClient().getPlayer().getMoney() < good.getPriceCredits())
            {
                getClient().SendPacket(new PROTOCOL_SHOP_BUY_ITEM_ACK(0x80001019, null, null ,0));
            }
            else
            {
                Item item = new Item
                {
                    Id = ItemsTable.items.Count,
                    ItemId = good.getItemId(),
                    Count = good.getQuantity(),
                    OwnerId = getClient().getPlayer().AccountID,
                    Type = good.getLifeType(),
                    ItemType = good.getItemType(),
                    Slot = good.getSlot()
                };

                ItemsTable.items[item.OwnerId].Add(item);
                getClient().getPlayer().setGp(getClient().getPlayer().getGp() - good.getPricePoints());
                getClient().getPlayer().setMoney(getClient().getPlayer().getMoney() - good.getPriceCredits());
                getClient().SendPacket(new PROTOCOL_SHOP_BUY_ITEM_ACK(0, item, getClient().getPlayer(), good.getItemType()));
            }
        }

        public Good getGoodItemById(int id)
        {
            foreach (Good item in GoodsParser.goods.Values) 
            {
                if (item.getGoodId() == id) 
                {
                    return item;
                }
            }
            return null;
        }
    }
}
