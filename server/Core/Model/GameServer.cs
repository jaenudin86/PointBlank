/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class GameServer
    {
        public string Name;
        public int Id;
        public int Type;
        public int MaxPlayers;
        public string Ip;
        public int Port;
    }
}
