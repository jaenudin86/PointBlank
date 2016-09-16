/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket
{
    public class PROTOCOL_MESSAGE_ALL_PLAYERS_ACK : SendPacket
    {
        private string message = "Clans Currently Unavailable";//message text
        private byte[] messageb;

        public static byte[] StrToByteArray(string str)
        {
            return new ASCIIEncoding().GetBytes(str);
        }

        public override void WriteImpl()
        {
            WriteH((short)2055);
            messageb = PROTOCOL_MESSAGE_ALL_PLAYERS_ACK.StrToByteArray(message);
            if (message.Length <= 0)
                return;
            WriteD(2);
            //WriteD(message.Length);
            WriteD(69);
            WriteB(messageb);
        }
    }
}
