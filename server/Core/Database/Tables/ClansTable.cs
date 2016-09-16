/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Core.Model;
using Core.Managers;

namespace Core.Database.Tables
{
    public class ClansTable
    {
        public static Dictionary<ulong, Clan> clans;
        public static List<Clan> list = new List<Clan>();

        public static void LoadTable()
        {
            try
            {
                clans = new Dictionary<ulong, Clan>();
                using (var reader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `clans`"))
                {
                    while (reader.Read())
                    {
                        Clan clan = new Clan()
                        {
                            Id = reader.GetUInt64("ClanID"),
                            OwnerId = reader.GetUInt64("OwnerID"),
                            Name = reader.GetString("Name"),
                            Rank = reader.GetInt16("Rank"),
                            Exp = reader.GetInt32("Exp"),
                            Logo1 = reader.GetInt16("Logo1"),
                            Logo2 = reader.GetInt16("Logo2"),
                            Logo3 = reader.GetInt16("Logo3"),
                            Logo4 = reader.GetInt16("Logo4"),
                            Color = reader.GetInt16("Color"),
                            MaxPlayersCount = reader.GetInt32("MaxPlayersCount"),
                            PlayersCount = reader.GetInt32("PlayersCount"),
                            Info = reader.GetString("Info"),
                            Notice = reader.GetString("Notice"),
                            //DateCreated = reader.GetInt32("DateCreated"),
                        };
                        clans.Add(clan.Id, clan);
                    }
                }
                Logger.Info("[ClansTable] Loaded {0} clans", clans.Count);
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }

        }

        public static void AddClan(ulong OwnerId, string Name, string Info)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "INSERT INTO clans(ClanID,OwnerID,Name,Rank,Exp,Logo1,Logo2,Logo3,Logo4,Color,MaxPlayersCount,PlayersCount,Info,Notice)VALUES('" + OwnerId + "', '" + OwnerId + "', '" + Name + "', '1', '0', '0', '0', '0', '0', '0', '50', '1', '', '');");
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players SET ClanID='" + OwnerId + "' WHERE PlayerID='" + OwnerId + "';");
                Logger.Info("[ClansTable] Clan successfully created");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }

        }

        public static void getClans()
        {
            try
            {
                List<Clan> list = new List<Clan>();
                using (var reader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `clans`"))
                {
                    while (reader.Read())
                    {
                        Clan clan = new Clan()
                        {
                            Id = reader.GetUInt64("ClanID"),
                            OwnerId = reader.GetUInt64("OwnerID"),
                            Name = reader.GetString("Name"),
                            Rank = reader.GetInt16("Rank"),
                            Exp = reader.GetInt32("Exp"),
                            Logo1 = reader.GetInt16("Logo1"),
                            Logo2 = reader.GetInt16("Logo2"),
                            Logo3 = reader.GetInt16("Logo3"),
                            Logo4 = reader.GetInt16("Logo4"),
                            Color = reader.GetInt16("Color"),
                            MaxPlayersCount = reader.GetInt32("MaxPlayersCount"),
                            PlayersCount = reader.GetInt32("PlayersCount"),
                            Info = reader.GetString("Info"),
                            Notice = reader.GetString("Notice"),
                        };
                        list.Add(clan);
                    }
                }
                Logger.Info("[ClansTable] Loaded {0} clan lists", list.Count);
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }

        }

    }
}
