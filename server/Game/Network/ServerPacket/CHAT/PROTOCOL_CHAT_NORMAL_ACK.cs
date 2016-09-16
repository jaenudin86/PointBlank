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
using Game.Network;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_CHAT_NORMAL_ACK : SendPacket
    {
        protected Chat chat;

        public PROTOCOL_CHAT_NORMAL_ACK(Chat chat)
        {
            this.chat = chat;
        }

        public override void WriteImpl()
        {
            WriteH((short)3093);
            WriteB(new byte[5]
            {
                (byte) 29,
                (byte) 0,
                (byte) 21,
                (byte) 12,
                (byte) 147
            });
            WriteS(chat.PlayerName, 34);
            WriteC((byte)0);
            WriteH((short)chat.Message.Length);
            WriteS(chat.Message, chat.Message.Length);
        }
    }
}
