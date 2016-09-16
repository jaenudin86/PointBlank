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
    class PROTOCOL_BATTLE_BOT_CHANGELEVEL_REQ : ReceivePacket
    {
        public PROTOCOL_BATTLE_BOT_CHANGELEVEL_REQ(GameNetwork Client, byte[] data)
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

            int AILevel = room.getRoomSlotByPlayer(room.getLeader()).getId() % 2 == 0 ? room.getAiLevel() + room.getBlueDeaths() / 20 : room.getAiLevel() + room.getRedDeaths() / 20;

            getClient().getPlayer().getRoom().setAiLevel(getClient().getPlayer().getRoom().getAiLevel() + 1);

            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_BATTLE_BOT_CHANGELEVEL_ACK(AILevel));
            }
        }
    }
}