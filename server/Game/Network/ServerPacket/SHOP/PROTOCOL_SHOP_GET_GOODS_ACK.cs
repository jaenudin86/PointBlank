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

namespace Game.Network.ServerPacket
{
    class PROTOCOL_SHOP_GET_GOODS_ACK : SendPacket
    {
        private List<Good> goods;

        public PROTOCOL_SHOP_GET_GOODS_ACK(List<Good> goods)
        {
            this.goods = goods;
        }

        public override void WriteImpl()
        {
            WriteH(0x20B);
            WriteD(goods.Count); // кол-во в магазине
            WriteD(goods.Count); // кол-во для отсылки
            WriteD(0); // сколько уже было отослано
            foreach (Good good in goods)
            {
                WriteD(good.getGoodId()); // айди в магазине
                WriteC((byte)good.getQuantity()); // Кол - во? quantity?
                WriteC((byte)good.getLifeType()); // Кол - во? quantity?
                WriteD(good.getPricePoints()); // цена в очках
                WriteD(good.getPriceCredits()); // цена в кредитах
                WriteC((byte)good.getStockType()); // Тип
            }
            WriteD(UNBUFFERED_STATE);
        }
    }
}
