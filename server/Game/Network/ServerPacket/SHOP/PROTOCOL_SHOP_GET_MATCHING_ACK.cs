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

namespace Game.Network.ServerPacket
{
    class PROTOCOL_SHOP_GET_MATCHING_ACK : SendPacket
    {
        private List<Good> items;

        public PROTOCOL_SHOP_GET_MATCHING_ACK(List<Good> items)
        {
            this.items = items;
        }
        public override void WriteImpl()
        {
            WriteH(0x20F);
            WriteD(items.Count); // кол-во в магазине
            WriteD(items.Count); // кол-во для отсылки
            WriteD(0); // сколько уже было отослано
            foreach (Good item in items)
            {
                WriteD(item.getGoodId()); // айди в магазине
                WriteD(item.getItemId()); // айди итемки
                WriteD(item.getQuantity()); // life_value
            }

            WriteD(UNBUFFERED_STATE);
        }
    }
}
