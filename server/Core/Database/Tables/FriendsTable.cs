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
    public class FriendsTable : Connector
    {
        public static Dictionary<ulong, List<Friend>> friends;

        public static void LoadTable()
        {
            try
            {
                FriendsTable.friends = new Dictionary<ulong, List<Friend>>();
                using (MySqlDataReader mySqlDataReader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players_friends`"))
                {
                    while (mySqlDataReader.Read())
                    {
                        Friend friend = new Friend()
                        {
                            OwnerID = mySqlDataReader.GetUInt64("OwnerID"),
                            FriendID = mySqlDataReader.GetUInt64("FriendID"),
                            Status = mySqlDataReader.GetInt32("Status")
                        };
                        if (!FriendsTable.friends.ContainsKey(friend.OwnerID))
                            FriendsTable.friends.Add(friend.OwnerID, new List<Friend>());
                        FriendsTable.friends[friend.OwnerID].Add(friend);
                    }
                }
                Core.Logger.Info("[FriendsTable] Loaded {0} friends", (object)FriendsTable.friends.Count);
            }
            catch (Exception ex)
            {
                Core.Logger.Error("[Error] {0}", (object)ex);
            }
        }
    }
}
