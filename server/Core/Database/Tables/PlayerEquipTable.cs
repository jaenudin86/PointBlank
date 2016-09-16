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
    public class PlayerEquipTable
    {
         public static Dictionary<ulong, PlayerEquip> players_equip;
         public static void LoadTable()
         {
             try
             {
                players_equip = new Dictionary<ulong, PlayerEquip>();
                using (var reader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players_equip`"))
                {
                    while (reader.Read())
                    {
                        PlayerEquip equips = new PlayerEquip()
                        {
                            PlayerID = reader.GetUInt64("PlayerID"),
                            WeaponPrimary = reader.GetInt32("WeaponPrimary"),
                            WeaponSecondary = reader.GetInt32("WeaponSecondary"),
                            WeaponMelee = reader.GetInt32("WeaponMelee"),
                            WeaponThrownNormal = reader.GetInt32("WeaponThrownNormal"),
                            WeaponThrownSpecial = reader.GetInt32("WeaponThrownSpecial"),
                            CharRed = reader.GetInt32("CharRed"),
                            CharBlue = reader.GetInt32("CharBlue"),
                            CharHelmet = reader.GetInt32("CharHelmet"),
                            CharDino = reader.GetInt32("CharDino"),
                            CharBeret = reader.GetInt32("CharBeret"),
                        };
                        players_equip.Add(equips.PlayerID, equips);
                    }
                }
                Logger.Info("[PlayersEquipTable] Loaded {0} players", players_equip.Count);
             }
             catch (Exception ex)
             {
                 Logger.Error("[Error] {0}", ex);
             }
         }

         public static void EquipAllSlots(ulong OwnerId, int Primary, int Secondary, int Melee, int WeaponThrownNormal, int WeaponThrownSpecial, int Red, int Blue, int Helmet, int Dino, int Beret)
         {
             try
             {
                 MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players_equip SET CharRed='" + Red + "', CharBlue='" + Blue + "', CharHelmet='" + Helmet + "', CharBeret='" + Beret + "', CharDino='" + Dino + "', WeaponPrimary='" + Primary + "', WeaponSecondary='" + Secondary + "', WeaponMelee='" + Melee + "', WeaponThrownNormal='" + WeaponThrownNormal + "', WeaponThrownSpecial='" + WeaponThrownSpecial + "' WHERE PlayerID='" + OwnerId + "';");

                 Logger.Info("[PlayersTable] Player successfully all slots equip");
             }
             catch (Exception ex)
             {
                 Logger.Error("[Error] {0}", ex);
             }
         }
         public static void EquipWeaponsSlots(ulong OwnerId, int Primary, int Secondary, int Melee, int WeaponThrownNormal, int WeaponThrownSpecial)
         {
             try
             {
                 MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players_equip SET WeaponPrimary='" + Primary + "', WeaponSecondary='" + Secondary + "', WeaponMelee='" + Melee + "', WeaponThrownNormal='" + WeaponThrownNormal + "', WeaponThrownSpecial='" + WeaponThrownSpecial + "' WHERE PlayerID='" + OwnerId + "';");

                 Logger.Info("[PlayersTable] Player successfully weapon slots equip");
             }
             catch (Exception ex)
             {
                 Logger.Error("[Error] {0}", ex);
             }
         }
         public static void EquipArmorsSlots(ulong OwnerId, int Red, int Blue, int Helmet, int Dino, int Beret)
         {
             try
             {
                 MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players_equip SET CharRed='" + Red + "', CharBlue='" + Blue + "', CharHelmet='" + Helmet + "', CharBeret='" + Dino + "', CharDino='" + Beret + "' WHERE PlayerID='" + OwnerId + "';");

                 Logger.Info("[PlayersTable] Player successfully armor slots equip");
             }
             catch (Exception ex)
             {
                 Logger.Error("[Error] {0}", ex);
             }
         }
    }
}
