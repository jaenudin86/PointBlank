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
using Core.Database.Tables;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_SHOP_BUY_ITEM_ACK : SendPacket
    {
        private uint error;
        private Item item;
        private Player player;
        private int Slot;
        private int GP;
        private int Money;

        public PROTOCOL_SHOP_BUY_ITEM_ACK(uint error, Item item, Player player, int ItemType)
        {
            this.error = error;
            this.player = player;
            this.item = item;
        }
        public override void WriteImpl()
        {

            Logger.Warn("Item Type: " + Slot);

            WriteH(0x213);
            if (error == 0)
            {
                WriteD(1);
                WriteD(Convert.ToInt32(DateTime.Now.ToString("yyMMddHHmm"))); //DateTimeUtil.getDateTime()
                Logger.Warn("TIME: " + Convert.ToInt32(DateTime.Now.ToString("yyMMddHHmm")));
                if (item.ItemType >= 0)
                {
                    if (item.ItemType < 5)
                    {
                        WriteD(0);
                        WriteD(1);
                        WriteD(0);
                        //Добавляем итем в базу данных
                        ItemsTable.AddItem(item.OwnerId, item.ItemId, item.ItemType, item.Type, item.Count);
                    }
                    else if (item.ItemType > 4 & item.ItemType < 10)
                    {
                        WriteD(1);
                        WriteD(0);
                        WriteD(0);
                        //Добавляем итем в базу данных
                        ItemsTable.AddItem(item.OwnerId, item.ItemId, item.ItemType, item.Type, item.Count);
                    }
                    else if (item.ItemType >= 10)
                    {
                        WriteD(0);
                        WriteD(0);
                        WriteD(1);

                        int newID = item.ItemId + 30 - 1000000000;
                        ItemsTable.AddItem(item.OwnerId, newID, item.ItemType, item.Type, item.Count);
                    }
                }
                Money = player.getMoney();
                GP = player.getGp();

                //Обновляем кол-во средств на аккаунте
                PlayersTable.UpdateMoney(player.PlayerID, GP, Money);

                if (item.ItemType == 3)
                {
                    WriteQ(0);
                }
                else
                {
                    WriteQ(item.ItemId);
                }

                WriteD(item.ItemId); //id
                WriteC((byte)item.Type); //settings weapon - type
                WriteD(item.Count); //settings weapon - count
                WriteD(player.getGp());
                WriteD(player.getMoney());
            }
            else
            {
                WriteD((int)error);
            }
        }
    }
}
