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
    class PROTOCOL_SHOP_GET_ITEMS_ACK : SendPacket
    {
        private List<Good> items;

        public PROTOCOL_SHOP_GET_ITEMS_ACK(List<Good> items)
        {
            this.items = items;
        }
        public override void WriteImpl()
        {
            WriteH(0x20D);
            WriteD(items.Count); // кол-во в магазине
            WriteD(items.Count); // кол-во для отсылки
            WriteD(0); // сколько уже было отослано
            foreach (Good item in items)
            {
                WriteD(item.getItemId()); // айди предмета
                WriteC((byte)item.getLifeType()); // life_Type время жизни (3 - постоянный, 2-на время, 1-на QTY, 0 - пусто)
                WriteC((byte)item.getQuantity()); // Кол - во? quantity?
                WriteC(1); // unk
                WriteC(0); // звание или перка?
            }

            WriteD(UNBUFFERED_STATE);
        }
    }
}
