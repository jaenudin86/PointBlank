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
using Core.Data.Parsers;
using Game.Network.ServerPacket;
using Game.Managers;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_LOBBY_CREATE_ROOM_REQ : ReceivePacket
    {
        private Room room;
        public PROTOCOL_LOBBY_CREATE_ROOM_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            Channel channel = getClient().getPlayer().getChannel();
            ReadD();
            room = new Room();
            getClient().getPlayer().setRoom(null);
            for (int id = 0; id < Channel.MAX_ROOMS_COUNT; id++) // TODO: Из модели
            {
                if (!channel.getRooms().ContainsKey(id))
                {
                    room.setId(id);
                    break;
                }
            }
            for (int i = 0; i < room.ROOM_SLOT.Length; ++i) // TODO: Нужно перенести в модель комнат
            {
                room.ROOM_SLOT[i] = new SLOT();
                room.ROOM_SLOT[i].setId(i);
            }
           // room.setRoomId(0);
            room.setName(ReadS(Room.ROOM_NAME_SIZE));
            room.setMapId(ReadC());
            ReadC(); // unk
            room.setStage4v4(ReadC());
            room.setType(ReadC());
            Logger.Warn("Room type: " + room.getType());
            ReadH();
            room.setSlots(ReadC());
            ReadC();
            room.setAllWeapons(ReadC());
            room.setRandomMap(ReadC());
            room.setSpecial(ReadC());
            ReadS(Player.MAX_NAME_SIZE);
            room.setKillMask(ReadC());
            ReadC(); // unk
            ReadC(); // unk
            ReadC(); // unk
            room.setLimit(ReadC());
            room.setSeeConf(ReadC());
            room.setAutobalans(ReadH());
            room.setPassword(ReadS(4));
            if (room.getSpecial() == 6)
            {
                room.setAiCount(ReadC());
                room.setAiLevel(ReadC());
            }
            room.setLeader(getClient().getPlayer());
            room.addPlayer(getClient().getPlayer());
            channel.addRoom(room);
            getClient().getPlayer().setRoom(room);
            getClient().getPlayer().getChannel().removePlayer(getClient().getPlayer());
            BattleHandler.CreateRoom(room.getId(), getClient().getPlayer());
            //BattleHandler.AddPlayer(getClient().getPlayer());
        }
        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_LOBBY_CREATE_ROOM_ACK(room));
        }
    }
}
