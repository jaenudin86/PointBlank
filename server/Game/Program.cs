/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Config;
using Core.Data.Parsers;
using Core.Data.Parsers.Missions;
using Core.Database.Tables;
using Core.Database;
using Game.Network;
using System.Threading;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Point Blank Game Server";
                Logger.Info("===============================================================================");
                Logger.Info("Point Blank Game Server");
                Logger.Info("Develop OZ-Network.RU - 2016");
                Logger.Info("===============================================================================");
                ConfigModel.Load();
                Logger.Warn("Load XML=======================================================================");
                ChannelsParser.Load();
                GoodsParser.Load();
                LevelUpParser.Load();
                TutorialParser.Load();
                Logger.Warn("Load DataBase==================================================================");
                Connector.Connect();
                ClansTable.LoadTable();
                AccountTable.LoadTable();
                ItemsTable.LoadTable();
                PlayersTable.LoadTable();
                QuestsTable.LoadTable();
                TitlesTable.LoadTable();
                PlayersConfigTable.LoadTable();
                PlayersStatsTable.LoadTable();
                TitlesTable.LoadTable();
                PlayerEquipTable.LoadTable();
                PlayersMedalsTable.LoadTable();
                Logger.Warn("Load Network===================================================================");
                NetworkS.Load();


            }
            catch(Exception e)
            {
                Logger.Error("[FATAL ERROR] " + e);
            }
        }
    }
}
