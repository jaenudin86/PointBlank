/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using Core.Database;
using Core.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Core.Database.Tables
{
    public class TitlesTable : Connector
    {
        public static Dictionary<ulong, Titles> titles;

        public static void LoadTable()
        {
            try
            {
                titles = new Dictionary<ulong, Titles>();
                using (MySqlDataReader mySqlDataReader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players_titles`"))
                {
                    while (mySqlDataReader.Read())
                    {
                        Titles user_titles = new Titles()
                        {
                            PlayerID = mySqlDataReader.GetUInt32("PlayerID"),
                            SlotCount = mySqlDataReader.GetInt32("SlotCount"),
                            titleEquiped1 = mySqlDataReader.GetInt32("titleEquiped1"),
                            titleEquiped2 = mySqlDataReader.GetInt32("titleEquiped2"),
                            titleEquiped3 = mySqlDataReader.GetInt32("titleEquiped3"),
                            title1 = mySqlDataReader.GetInt32("title1"),
                            title2 = mySqlDataReader.GetInt32("title2"),
                            title3 = mySqlDataReader.GetInt32("title3"),
                            title4 = mySqlDataReader.GetInt32("title4"),
                            title5 = mySqlDataReader.GetInt32("title5"),
                            title6 = mySqlDataReader.GetInt32("title6"),
                            title7 = mySqlDataReader.GetInt32("title7"),
                            title8 = mySqlDataReader.GetInt32("title8"),
                            title9 = mySqlDataReader.GetInt32("title9"),
                            title10 = mySqlDataReader.GetInt32("title10"),
                            title11 = mySqlDataReader.GetInt32("title11"),
                            title12 = mySqlDataReader.GetInt32("title12"),
                            title13 = mySqlDataReader.GetInt32("title13"),
                            title14 = mySqlDataReader.GetInt32("title14"),
                            title15 = mySqlDataReader.GetInt32("title15"),
                            title16 = mySqlDataReader.GetInt32("title16"),
                            title17 = mySqlDataReader.GetInt32("title17"),
                            title18 = mySqlDataReader.GetInt32("title18"),
                            title19 = mySqlDataReader.GetInt32("title19"),
                            title20 = mySqlDataReader.GetInt32("title20"),
                            title21 = mySqlDataReader.GetInt32("title21"),
                            title22 = mySqlDataReader.GetInt32("title22"),
                            title23 = mySqlDataReader.GetInt32("title23"),
                            title24 = mySqlDataReader.GetInt32("title24"),
                            title25 = mySqlDataReader.GetInt32("title25"),
                            title26 = mySqlDataReader.GetInt32("title26"),
                            title27 = mySqlDataReader.GetInt32("title27"),
                            title28 = mySqlDataReader.GetInt32("title28"),
                            title29 = mySqlDataReader.GetInt32("title29"),
                            title30 = mySqlDataReader.GetInt32("title30"),
                            title31 = mySqlDataReader.GetInt32("title31"),
                            title32 = mySqlDataReader.GetInt32("title32"),
                            title33 = mySqlDataReader.GetInt32("title33"),
                            title34 = mySqlDataReader.GetInt32("title34"),
                            title35 = mySqlDataReader.GetInt32("title35"),
                            title36 = mySqlDataReader.GetInt32("title36"),
                            title37 = mySqlDataReader.GetInt32("title37"),
                            title38 = mySqlDataReader.GetInt32("title38"),
                            title39 = mySqlDataReader.GetInt32("title39"),
                            title40 = mySqlDataReader.GetInt32("title40"),
                            title41 = mySqlDataReader.GetInt32("title41"),
                            title42 = mySqlDataReader.GetInt32("title42"),
                            title43 = mySqlDataReader.GetInt32("title43"),
                            title44 = mySqlDataReader.GetInt32("title44"),
                        };
                        titles.Add(user_titles.PlayerID, user_titles);
                    }
                }
                Core.Logger.Info("[TitlesTable] Loaded {0} titles", titles.Count);
            }
            catch (Exception ex)
            {
                Core.Logger.Error("[Error] {0}", (object)ex);
            }
        }
    }
}
