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
    class PROTOCOL_ROOM_CHANGE_INFO_REQ : ReceivePacket
    {

        public PROTOCOL_ROOM_CHANGE_INFO_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            ReadD();
            getClient().getPlayer().getRoom().setName(ReadS(23));
            getClient().getPlayer().getRoom().setMapId(ReadC());
            ReadC();
            getClient().getPlayer().getRoom().setStage4v4(ReadC());
            getClient().getPlayer().getRoom().setType(ReadC());
            ReadC();
            ReadC();
            ReadC();
            ReadC();
            getClient().getPlayer().getRoom().setAllWeapons(ReadC());
            getClient().getPlayer().getRoom().setRandomMap(ReadC());
            getClient().getPlayer().getRoom().setSpecial(ReadC());
            ReadS(33);
            getClient().getPlayer().getRoom().setKillMask(ReadC());
            ReadC();
            ReadC();
            ReadC();
            getClient().getPlayer().getRoom().setLimit(ReadC());
            getClient().getPlayer().getRoom().setSeeConf(ReadC());
            getClient().getPlayer().getRoom().setAutobalans(ReadH());
            getClient().getPlayer().getRoom().setAiCount(ReadC());
            getClient().getPlayer().getRoom().setAiLevel(ReadC());
        }

        public override void RunImpl()
        {
            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_BATTLE_ROOMINFO_ACK(getClient().getPlayer().getRoom()));
            }
        }
    }
}
