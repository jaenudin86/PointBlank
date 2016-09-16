using Core.Database;
using Core.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Core.Database.Tables
{
    /* 
     * Надо разбираться - тут походу пиздец
     */
    public class ClientConfigTable : Connector
    {
        public static Dictionary<int, List<PlayerConfig>> configuration;

        public static void LoadTable()
        {
            try
            {
                configuration = new Dictionary<int, List<PlayerConfig>>();
                using (MySqlDataReader mySqlDataReader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players_config`"))
                {
                    while (mySqlDataReader.Read())
                    {
                        PlayerConfig player_config = new PlayerConfig()
                        {
                            owner_id = mySqlDataReader.GetInt32("OwnerId"),

                        };
                        if (!configuration.ContainsKey(player_config.owner_id))
                        {

                            configuration.Add(player_config.owner_id, new List<PlayerConfig>());

                        }
                    }
                }
                Core.Logger.Info("[ClientConfigTable] Loaded {0} clients configurations", configuration.Count);
            }
            catch (Exception ex)
            {
                Core.Logger.Error("[Error] {0}", (object)ex);
            }
        }
    }
}
