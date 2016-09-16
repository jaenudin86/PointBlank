/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Config
{
    public class ConfigModel
    {
        public static string HOST, BATTLE_SERVER;
        public static int PORT;
        public static string DB_IP, DB_NAME, DB_USER, DB_PASS;
        public static int DB_PORT;
        public static int Ivent;

        public static void Load()
        {
            ConfigLoader config = new ConfigLoader(@"Settings.ini");
            HOST = config.getValue("Host", "127.0.0.1");
            PORT = int.Parse(config.getValue("Port", "39190"));
            DB_IP = config.getValue("DataBaseHost", "localhost");
            DB_NAME = config.getValue("DataBaseName", "pointblank");
            DB_USER = config.getValue("DataBaseUser", "root");
            DB_PASS = config.getValue("DataBasePass", "1234567");
            DB_PORT = int.Parse(config.getValue("DataBasePort", "3306"));
            Ivent = int.Parse(config.getValue("Ivent", "0"));
            BATTLE_SERVER = config.getValue("BattleHost", "127.0.0.1");
        }
    }
}
