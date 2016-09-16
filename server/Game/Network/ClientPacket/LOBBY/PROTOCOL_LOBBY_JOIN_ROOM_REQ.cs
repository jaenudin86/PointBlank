/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using Core.Model;
using Game.Network.ServerPacket;
using Game.Network.ServerPacket.LOBBY;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_LOBBY_JOIN_ROOM_REQ : ReceivePacket
    {
        private int roomId;
        private string password;

        public PROTOCOL_LOBBY_JOIN_ROOM_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            roomId = ReadD();
            password = ReadS(4);
        }

        public override void RunImpl()
        {
            Room room = getClient().getPlayer().getChannel().getRoom(roomId);
            if (room == null)
            {
                getClient().SendPacket(new PROTOCOL_LOBBY_JOIN_ROOM_ACK(null, 0, 0x80001004));
                return;
            }

            if(room.getPassword() != null & room.getPassword() != password)
            {
                getClient().SendPacket(new PROTOCOL_LOBBY_ROOM_PASSWORD_ERROR_ACK());
                return;
            }

            getClient().getPlayer().setRoom(room);
            room.addPlayer(getClient().getPlayer());

            SLOT roomSlot = room.getRoomSlotByPlayer(getClient().getPlayer());
            if(roomSlot == null)
            {
                getClient().SendPacket(new PROTOCOL_LOBBY_JOIN_ROOM_ACK(null, 0, 0x80001004));
            }

            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_ROOM_PLAYER_ENTER_ACK(roomSlot));
            }

            getClient().getPlayer().getChannel().removePlayer(getClient().getPlayer());
            getClient().SendPacket(new PROTOCOL_LOBBY_JOIN_ROOM_ACK(room, roomSlot.getId(), 0));
            BattleHandler.AddPlayer(getClient().getPlayer());
        }
    }
}
