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
    public class PlayersConfigTable : Connector
    {
        public static Dictionary<ulong, PlayerConfig> configuration;

        public static void LoadTable()
        {
            try
            {
                configuration = new Dictionary<ulong, PlayerConfig>();
                using (MySqlDataReader mySqlDataReader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players_config`"))
                {
                    while (mySqlDataReader.Read())
                    {
                        PlayerConfig player_config = new PlayerConfig()
                        {
                            PlayerID = mySqlDataReader.GetUInt64("PlayerID"),
                            Config = mySqlDataReader.GetInt32("Config"),
                            Blood = mySqlDataReader.GetInt32("Blood"),
                            Visibility = mySqlDataReader.GetInt32("Visibility"),
                            Mao = mySqlDataReader.GetInt32("Mao"),
                            Audio1 = mySqlDataReader.GetInt32("Audio1"),
                            Audio2 = mySqlDataReader.GetInt32("Audio2"),
                            AudioEnable = mySqlDataReader.GetInt32("AudioEnable"),
                            MouseSensitivity = mySqlDataReader.GetInt32("MouseSensitivity"),
                            Vision = mySqlDataReader.GetInt32("Vision"),

                        };
                        configuration.Add(player_config.PlayerID, player_config);
                    }
                }
                Core.Logger.Info("[PLayersConfigTable] Loaded {0} clients configurations", configuration.Count);
            }
            catch (Exception ex)
            {
                Core.Logger.Error("[Error] {0}", (object)ex);
            }
        }

        public static void SaveConfigs(ulong PlayerID, int Config, int Blood, int Visibility, int Mao, int Audio1, int Audio2, int AudioEnable, int MouseSensitivity, int Vision)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players_config SET Config='" + Config + "', Blood='" + Blood + "', Visibility='" + Visibility + "', Mao='" + Mao + "', Audio1='" + Audio1 + "', Audio2='" + Audio2 + "', AudioEnable='" + AudioEnable + "', MouseSensitivity='" + MouseSensitivity + "', Vision='" + Vision + "' WHERE PlayerID='" + PlayerID + "';");

                Logger.Info("[ClientConfigTable] Player successfully update config");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }
    }
}
