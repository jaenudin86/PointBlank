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
using Core.Data.Parsers;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_CHAT_NORMAL_REQ : ReceivePacket
    {
        private Chat chat = new Chat();
        private int len;
        public int num { get; set; }

        public PROTOCOL_CHAT_NORMAL_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            
            chat.chatType = ReadH();
            len = (int)ReadH();
            chat.Message = ReadS(len);
            chat.PlayerName = getClient().getPlayer().PlayerName;
        }

        public override void RunImpl()
        {
            Player player = getClient().getPlayer();
            Room room = player.getRoom();
            switch (this.chat.chatType)
            {
                case (short)1:
                case (short)6:
                if (room == null)
                {
                    Channel channel = ChannelsParser._servers[getClient().getPlayer().getChannel().getId()];
                    if (channel == null)
                        break;
                    for (int index = 0; index < channel.getPlayers().Count; ++index)
                    { 
                        if (channel.getPlayers() != null)
                            foreach (Player member in channel.getPlayers().Values)
                            {
                                member.getClient().SendPacket(new PROTOCOL_CHAT_NORMAL_ACK(chat));
                            }
                    }
                    break;
                }
                /*for (num = 0; num < 16; ++num)
                {
                    Player playerBySlot = room.getRoomSlotByPlayer(num);
                } */

                break;
            }
        }

    }
}
