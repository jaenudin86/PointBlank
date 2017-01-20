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
    public class PlayersTable
    {
        public static Dictionary<ulong, Player> players;
        public static Dictionary<string, Player> checkPlayer;
        public static void LoadTable()
        {
            try
            {
                players = new Dictionary<ulong, Player>();
                using (var reader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players`"))
                {
                    while (reader.Read())
                    {
                        Player player = new Player()
                        {
                            PlayerID = reader.GetUInt64("PlayerID"),
                            AccountID = reader.GetUInt64("AccountID"),
                            PlayerName = reader.GetString("Name"),
                            Rank = reader.GetInt32("Rank"),
                            PC_Cafe = reader.GetInt32("PC_Cafe"),
                            Emblem = reader.GetInt32("Emblem"),
                            Exp = reader.GetInt32("Exp"),
                            GP = reader.GetInt32("GP"),
                            Money = reader.GetInt32("Money"),
                            Clan = ClansManager.Load().getClanById(reader.GetUInt64("ClanID")),
                            Effect1 = reader.GetInt32("Effect1"), 
                            Effect2 = reader.GetInt32("Effect2"),
                            Effect3 = reader.GetInt32("Effect3"),
                            Effect4 = reader.GetInt32("Effect4"),
                            Effect5 = reader.GetInt32("Effect5"),
                        };
                        players.Add(player.AccountID, player);
                    }
                }
                Logger.Info("[PlayersTable] Loaded {0} players", players.Count);
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }

        public static void updatePlayerInfo(ulong PlayerID)
        {
            try
            {
                using (var reader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players` WHERE PlayerID='" + PlayerID + "'';"))
                {
                    while (reader.Read())
                    {
                        Player player = new Player()
                        {
                            PlayerID = reader.GetUInt64("PlayerID"),
                            AccountID = reader.GetUInt64("AccountID"),
                            PlayerName = reader.GetString("Name"),
                            Rank = reader.GetInt32("Rank"),
                            PC_Cafe = reader.GetInt32("PC_Cafe"),
                            Emblem = reader.GetInt32("Emblem"),
                            Exp = reader.GetInt32("Exp"),
                            GP = reader.GetInt32("GP"),
                            Money = reader.GetInt32("Money"),
                            Clan = ClansManager.Load().getClanById(reader.GetUInt64("ClanID")),
                            Effect1 = reader.GetInt32("Effect1"), 
                            Effect2 = reader.GetInt32("Effect2"),
                            Effect3 = reader.GetInt32("Effect3"),
                            Effect4 = reader.GetInt32("Effect4"),
                            Effect5 = reader.GetInt32("Effect5"),
                        };
                        players.Remove(PlayerID);// удаляем значение из коллекции
                        players.Add(player.AccountID, player);// добавляем обновленное значение в коллекцию
                    }
                }
                Logger.Info("[PlayersTable] Player successfully update");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }

        public static void UpdateMoney(ulong PlayerID, int GP, int Money)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players SET GP='" + GP + "', Money='" + Money + "' WHERE PlayerID='" + PlayerID + "';");

                Logger.Info("[PlayersTable] Player successfully update Money/GP");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }

        public static void UpdateGP_Exp(ulong PlayerID, int GP, int Exp)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players SET Exp='" + Exp + "', GP='" + GP + "' WHERE PlayerID='" + PlayerID + "';");

                Logger.Info("[PlayersTable] Player successfully update GP and Exp");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }

        public static void UpdateRank(ulong PlayerID, int Rank)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players SET Rank='" + Rank + "' WHERE PlayerID='" + PlayerID + "';");

                Logger.Info("[PlayersTable] Player successfully update Rank");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }


        public static void CreatePlayer(ulong AccountID, string Name)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players SET Name='" + Name + "' WHERE AccountID='" + AccountID + "';");

                Logger.Info("[PlayersTable] Player successfully create.");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }

        public static void CheckPlayer(string Name)
        {
            try
            {
                checkPlayer = new Dictionary<string, Player>();
                using (var reader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players` WHERE Name='" + Name + "';"))
                {
                    while (reader.Read())
                    {
                        Player player2 = new Player()
                        {
                            PlayerID = reader.GetUInt64("PlayerID"),
                            AccountID = reader.GetUInt64("AccountID"),
                            PlayerName = reader.GetString("Name"),
                            Rank = reader.GetInt32("Rank"),
                            PC_Cafe = reader.GetInt32("PC_Cafe"),
                            Emblem = reader.GetInt32("Emblem"),
                            Exp = reader.GetInt32("Exp"),
                            GP = reader.GetInt32("GP"),
                            Money = reader.GetInt32("Money"),
                            Clan = ClansManager.Load().getClanById(reader.GetUInt64("ClanID")),
                        };
                        checkPlayer.Add(player2.PlayerName, player2);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }
    }
}
