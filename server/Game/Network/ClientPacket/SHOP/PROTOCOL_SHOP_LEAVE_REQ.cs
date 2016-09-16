/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using Game.Network.ServerPacket;
using Core.Model;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_SHOP_LEAVE_REQ : ReceivePacket
    {
        public PROTOCOL_SHOP_LEAVE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
        }
        public override void RunImpl()
        {
            Room room = getClient().getPlayer().getRoom();
            if (room == null)
            {
                Logger.Info("You aren't in a room!");

                getClient().SendPacket(new PROTOCOL_SHOP_LEAVE_ACK());
            }

            else
            {
                Logger.Info("You are in a room!");
                getClient().getPlayer().getRoom().getRoomSlotByPlayer(getClient().getPlayer()).setState(SLOT_STATE.SLOT_STATE_NORMAL);
                getClient().SendPacket(new PROTOCOL_SHOP_LEAVE_ACK());
            }

        }
    }
}