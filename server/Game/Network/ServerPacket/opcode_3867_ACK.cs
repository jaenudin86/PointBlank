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

namespace Game.Network.ServerPacket
{
    class opcode_3867_ACK2 : SendPacket
    {
        public override void WriteImpl()
        {
            WriteH(0xF1B);
            WriteC(1); // что это?
            // _log.info("SEND: " + _r.getTimeLostInSec()); Все норм, если по стандарту = 600 сек.
            // Почему клиент показывает 10 мин и 1 сек, и по стандарту не стартует таймер!!!
            WriteD(600);
            WriteH(1); // Может запустить таймер?
        }
    }
}
