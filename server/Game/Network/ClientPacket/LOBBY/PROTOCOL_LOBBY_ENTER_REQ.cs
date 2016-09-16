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
using System.Threading;
using Game.Network.ServerPacket.MESSAGES;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_LOBBY_ENTER_REQ : ReceivePacket
    {
        public PROTOCOL_LOBBY_ENTER_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            
        }
        public override void RunImpl()
        {
            Player player = getClient().getPlayer();
            Room room = getClient().getPlayer().getRoom();
            if (room != null && player != null)
            {
                if(room.getPlayers().Count == 0)
                {
                    player.getChannel().removeRoom(room);
                }

                room.removePlayer(player);
                player.getChannel().addPlayer(player);
                /* Если в комнате пусто, удаляем с боевого сервера */
                if (room.getPlayers().Count == 0)
                {
                    BattleHandler.DeleteRoom(room);
                }
            }

            getClient().getPlayer().channel.RemoteEmptyRooms();//удаляем пустые комнаты с канала

            getClient().SendPacket(new PROTOCOL_LOBBY_ENTER_ACK());

            getClient().SendPacket(new PROTOCOL_MESSENGER_NOTE_LIST_ACK());
            getClient().SendPacket(new PROTOCOL_MESSENGER_CHECK_ACK());

            /*Thread.Sleep(5000);
            getClient().SendPacket(new PROTOCOL_MESSAGE_ALL_PLAYERS_ACK()); */
            getClient().getPlayer().setRoom(null); 
        }
    }
}
