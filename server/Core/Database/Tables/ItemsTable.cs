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

namespace Core.Database.Tables
{
    public class ItemsTable
    {
        public static Dictionary<ulong, List<Item>> items;
        public static void LoadTable()
        {
            try
            {
                items = new Dictionary<ulong, List<Item>>();
                using (var reader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `items`"))
                {
                    while (reader.Read())
                    {
                        Item item = new Item()
                        {
                            Id = reader.GetInt32("ObjectID"),
                            OwnerId = reader.GetUInt64("OwnerID"),
                            ItemId = reader.GetInt32("ItemID"),
                            Slot = reader.GetInt32("Slot"),
                            //Equip = reader.GetInt32("Equip"),
                            Type = reader.GetInt32("Type"),
                            Count = reader.GetInt32("Count")

                        };
                        if (!items.ContainsKey(item.OwnerId))
                        {

                            items.Add(item.OwnerId, new List<Item>());

                        }
                        items[item.OwnerId].Add(item);
                    }
                }
                Logger.Info("[ItemsTable] Loaded {0} items", items.Count);
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }

        }

        public static void AddItem(ulong OwnerId, int ItemId, int Slot, int ItemType, int Count)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "INSERT INTO items(OwnerID,ItemID,Slot,Type,Count)VALUES('" + OwnerId + "','" + ItemId + "','" + Slot + "','" + ItemType + "','" + Count + "')");
                Logger.Info("[ItemsTable] Item successfully added");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }

        }

        public static void DelItem(int ItemId)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "DELETE FROM `items` WHERE (`ObjectID`='"+ ItemId +"')");
                Logger.Info("[ItemsTable] Item successfully delete");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }
        public static void UpdateQuantity(ulong ObjectID, int Count)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE `items` SET `Count`='"+ Count +"' WHERE (`ObjectID`='"+ ObjectID +"')");
                Logger.Info("[ItemsTable] Item successfully update quantity");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }
    }
}
