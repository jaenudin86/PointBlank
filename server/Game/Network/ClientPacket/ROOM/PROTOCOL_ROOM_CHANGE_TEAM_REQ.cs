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
using Game.Network.ServerPacket;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_ROOM_CHANGE_TEAM_REQ : ReceivePacket
    {
        private int team;

        public PROTOCOL_ROOM_CHANGE_TEAM_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            team = ReadD();
        }

        public override void RunImpl()
        {
            Room room = getClient().getPlayer().getRoom();
            Player player = getClient().getPlayer();
            getClient().SendPacket(new PROTOCOL_ROOM_CHANGE_TEAM_ACK(player, room, 0, 1, team));

            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_ROOM_CHANGE_TEAM_ACK(getClient().getPlayer(),  room, 0 , 1 , team));
                member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(room));
            }

        }
    }
}
