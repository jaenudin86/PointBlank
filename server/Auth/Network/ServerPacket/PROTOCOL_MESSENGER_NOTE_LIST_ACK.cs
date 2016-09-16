/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointBlank.Network.ServerPacket
{
    class PROTOCOL_MESSENGER_NOTE_LIST_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0x1A5);
			//TODO: Цикл
            WriteD(0);
            WriteD(0);
            WriteD(2);
            WriteD(3);
            WriteC(4);
            WriteC(5);
            WriteC(6);
            WriteC(Convert.ToByte(5));//длина ника отправителя
            WriteC(33);//Длина сообщения по идее
            WriteS("Admin", 5);
            WriteS("Hello! Welcome to PTS-Server OZ-Network.RU!", 43);
            Console.WriteLine("send messenger");
        }
    }
}