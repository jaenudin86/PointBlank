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
using MySql.Data.MySqlClient;
using Core.Managers;

namespace Core.Database.Tables
{
    public class PlayersMedalsTable
    {
        public static Dictionary<ulong, PlayerMedals> playersMedals;
        public static void LoadTable()
        {
            try
            {
                playersMedals = new Dictionary<ulong, PlayerMedals>();
                using (var reader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players_medals`"))
                {
                    while (reader.Read())
                    {
                        PlayerMedals medals = new PlayerMedals()
                        {
                            PlayerID = reader.GetUInt64("PlayerID"),
                            Ribbons = reader.GetInt32("Ribbons"),
                            Badges = reader.GetInt32("Badges"),
                            Medals = reader.GetInt32("Medals"),
                            MasterMedals = reader.GetInt32("MasterMedals"),
                        };
                        playersMedals.Add(medals.PlayerID, medals);
                    }
                }
                Logger.Info("[PlayersMedalsTable] Loaded {0} players", playersMedals.Count);
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }
    }
}
