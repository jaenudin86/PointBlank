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
    public class PlayersStatsTable : Connector
    {
        public static Dictionary<ulong, PlayerStats> statistics;

        public static void LoadTable()
        {
            try
            {
                statistics = new Dictionary<ulong, PlayerStats>();
                using (MySqlDataReader mySqlDataReader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players_stats`"))
                {
                    while (mySqlDataReader.Read())
                    {
                        PlayerStats player_stats = new PlayerStats()
                        {
                            PlayerID = mySqlDataReader.GetUInt64("PlayerID"),
                            Fights = mySqlDataReader.GetInt32("Fights"),
                            Wins = mySqlDataReader.GetInt32("Wins"),
                            Losts = mySqlDataReader.GetInt32("Losts"),
                            Kills = mySqlDataReader.GetInt32("Kills"),
                            Headshots = mySqlDataReader.GetInt32("Headshots"),
                            Deaths = mySqlDataReader.GetInt32("Deaths"),
                            Escapes = mySqlDataReader.GetInt32("Escapes"),
                            SeasonFights = mySqlDataReader.GetInt32("SeasonFights"),
                            SeasonWins = mySqlDataReader.GetInt32("SeasonWins"),
                            SeasonLosts = mySqlDataReader.GetInt32("SeasonLosts"),
                            SeasonKills = mySqlDataReader.GetInt32("SeasonKills"),
                            SeasonHeadshots = mySqlDataReader.GetInt32("SeasonHeadshots"),
                            SeasonDeaths = mySqlDataReader.GetInt32("SeasonDeaths"),
                            SeasonEscapes = mySqlDataReader.GetInt32("SeasonEscapes"),
                        };
                        statistics.Add(player_stats.PlayerID, player_stats);
                    }
                }
                Core.Logger.Info("[PlayerStatsTable] Loaded {0} players statistics", statistics.Count);
            }
            catch (Exception ex)
            {
                Core.Logger.Error("[Error] {0}", (object)ex);
            }
        }
        public static void UpdateStats(ulong PlayerID, int Kills, int Headshots, int SeasonDeaths)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players_stats SET SeasonKills='" + Kills + "', SeasonHeadshots='" + Headshots + "', SeasonDeaths='" + SeasonDeaths + "' WHERE PlayerID='" + PlayerID + "';");

                Logger.Info("[PlayersStatsTable] Player successfully update stats");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }

        public static void UpdateEscapes(ulong PlayerID, int Escapes)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players_stats SET SeasonEscapes='" + Escapes + "' WHERE PlayerID='" + PlayerID + "';");

                Logger.Info("[PlayersStatsTable] Player successfully update stats");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }
    }
}
