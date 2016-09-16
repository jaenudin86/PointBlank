using Core.Database;
using Core.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Core.Database.Tables
{
    public class MissionsTable : Connector
    {
        public static Dictionary<int, List<Missions>> player_missions;

        public static void LoadTable()
        {
            try
            {
                player_missions = new Dictionary<int, List<Missions>>();
                using (MySqlDataReader mySqlDataReader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players_missions`"))
                {
                    while (mySqlDataReader.Read())
                    {
                        Missions missions = new Missions()
                        {
                            owner_id = mySqlDataReader.GetInt32("owner_id"),

                        };
                        missions.cards_tutorial[1] = mySqlDataReader.GetInt32("mission_1");
                        missions.cards_tutorial[2] = mySqlDataReader.GetInt32("mission_2");
                        missions.cards_tutorial[3] = mySqlDataReader.GetInt32("mission_3");
                        missions.cards_tutorial[4] = mySqlDataReader.GetInt32("mission_4");
                        missions.cards_tutorial[5] = mySqlDataReader.GetInt32("mission_5");
                        missions.cards_tutorial[6] = mySqlDataReader.GetInt32("mission_6");
                        missions.cards_tutorial[7] = mySqlDataReader.GetInt32("mission_7");
                        missions.cards_tutorial[8] = mySqlDataReader.GetInt32("mission_8");
                        missions.cards_tutorial[9] = mySqlDataReader.GetInt32("mission_9");
                        missions.cards_tutorial[10] = mySqlDataReader.GetInt32("mission_10");

                        if (!player_missions.ContainsKey(missions.owner_id))
                        {

                            player_missions.Add(missions.owner_id, new List<Missions>());

                        }
                    }
                }
                Core.Logger.Info("[MissionsTable] Loaded {0} missions", player_missions.Count);
            }
            catch (Exception ex)
            {
                Core.Logger.Error("[Error] {0}", (object)ex);
            }
        }
    }
}
