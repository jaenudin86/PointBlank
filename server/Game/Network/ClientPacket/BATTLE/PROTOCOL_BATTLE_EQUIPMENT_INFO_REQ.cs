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
using Game.Managers;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BATTLE_EQUIPMENT_INFO_REQ : ReceivePacket
    {
        public PROTOCOL_BATTLE_EQUIPMENT_INFO_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            
        }
        public override void RunImpl()
        {
            Player player = getClient().getPlayer();
            Room room = player.getRoom();
            room.getRoomSlotByPlayer(getClient().getPlayer()).setState(SLOT_STATE.SLOT_STATE_RENDEZVOUS);
           // RoomManager.ChangeRoomState(ROOM_STATE.ROOM_STATE_RENDEZVOUS, player, room);
            getClient().SendPacket(new PROTOCOL_BATTLE_EQUIPMENT_INFO_ACK());
        }
    }
}
