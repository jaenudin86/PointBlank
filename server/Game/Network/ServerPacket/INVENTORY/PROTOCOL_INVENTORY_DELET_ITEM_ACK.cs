/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using Game.Network;

namespace Game.Network.ServerPacket
{
    internal class PROTOCOL_INVENTORY_DELET_ITEM_ACK : SendPacket
    {
        private long ID;

        public PROTOCOL_INVENTORY_DELET_ITEM_ACK(long ID)
        {
            this.ID = ID;
        }

        public override void WriteImpl()
        {
            WriteH(535);
            this.WriteD(1);
            WriteQ(ID);
            byte[] numArray = new byte[13];
            numArray[8] = (byte)2;
            WriteB(numArray);
        }
    }
}