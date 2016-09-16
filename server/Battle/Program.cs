/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Point Blank Battle Server";
            Logger.Info("===============================================================================");
            Logger.Info("Point Blank Battle Server");
            Logger.Info("Develop OZ-Network.RU - 2015");
            Logger.Info("===============================================================================");
            Logger.Warn("Load Network===================================================================");
            NetworkS.Load();
            Console.ReadKey();
        }
    }
}
