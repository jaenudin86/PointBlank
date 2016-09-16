/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using Core.Model;
using Game.Network;

namespace Game.Network.ServerPacket
{
    internal class PROTOCOL_INVENTORY_ADD_ITEM_ACK : SendPacket
    {
        private int NewID;
        Player player;

        public PROTOCOL_INVENTORY_ADD_ITEM_ACK(int NewID)
        {
            this.NewID = NewID;
        }

        public override void WriteImpl()
        {
            Item item = player.getItemById(NewID);
            WriteH(3588);
            WriteC(1);
            WriteD(0);
            WriteD(0);
            WriteD(1);
            WriteQ((long)item.Id);
            WriteD((int)item.ItemId);
            WriteB(new byte[4]
            {
                (byte) 2,
                (byte) 1,
                (byte) 0,
                (byte) 0
            });
            WriteC(1);
        }
    }
}