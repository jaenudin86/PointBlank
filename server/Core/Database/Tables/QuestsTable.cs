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
    public class QuestsTable
    {
        public static Dictionary<ulong, QuestCardSet> quests;
        public static void LoadTable()
        {
            try
            {
                quests = new Dictionary<ulong, QuestCardSet>();
                using (var reader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players_quests`"))
                {
                    while (reader.Read())
                    {
                        QuestCardSet quest = new QuestCardSet()
                        {
                            PlayerID = reader.GetUInt64("PlayerID"),
                            MissionID = reader.GetUInt16("MissionID"),
                            CardID = reader.GetUInt16("CardID"),//отсчет идет от 0 до 9
                            Card1_1 = reader.GetUInt16("Card1_1"),
                            Card1_2 = reader.GetUInt16("Card1_2"),
                            Card1_3 = reader.GetUInt16("Card1_3"),
                            Card1_4 = reader.GetUInt16("Card1_4"),
                            Card2_1 = reader.GetUInt16("Card2_1"),
                            Card2_2 = reader.GetUInt16("Card2_2"),
                            Card2_3 = reader.GetUInt16("Card2_3"),
                            Card2_4 = reader.GetUInt16("Card2_4"),
                            Card3_1 = reader.GetUInt16("Card3_1"),
                            Card3_2 = reader.GetUInt16("Card3_2"),
                            Card3_3 = reader.GetUInt16("Card3_3"),
                            Card3_4 = reader.GetUInt16("Card3_4"),
                            Card4_1 = reader.GetUInt16("Card4_1"),
                            Card4_2 = reader.GetUInt16("Card4_2"),
                            Card4_3 = reader.GetUInt16("Card4_3"),
                            Card4_4 = reader.GetUInt16("Card4_4"),
                            Card5_1 = reader.GetUInt16("Card5_1"),
                            Card5_2 = reader.GetUInt16("Card5_2"),
                            Card5_3 = reader.GetUInt16("Card5_3"),
                            Card5_4 = reader.GetUInt16("Card5_4"),
                            Card6_1 = reader.GetUInt16("Card6_1"),
                            Card6_2 = reader.GetUInt16("Card6_2"),
                            Card6_3 = reader.GetUInt16("Card6_3"),
                            Card6_4 = reader.GetUInt16("Card6_4"),
                            Card7_1 = reader.GetUInt16("Card7_1"),
                            Card7_2 = reader.GetUInt16("Card7_2"),
                            Card7_3 = reader.GetUInt16("Card7_3"),
                            Card7_4 = reader.GetUInt16("Card7_4"),
                            Card8_1 = reader.GetUInt16("Card8_1"),
                            Card8_2 = reader.GetUInt16("Card8_2"),
                            Card8_3 = reader.GetUInt16("Card8_3"),
                            Card8_4 = reader.GetUInt16("Card8_4"),
                            Card9_1 = reader.GetUInt16("Card9_1"),
                            Card9_2 = reader.GetUInt16("Card9_2"),
                            Card9_3 = reader.GetUInt16("Card9_3"),
                            Card9_4 = reader.GetUInt16("Card9_4"),
                            Card10_1 = reader.GetUInt16("Card10_1"),
                            Card10_2 = reader.GetUInt16("Card10_2"),
                            Card10_3 = reader.GetUInt16("Card10_3"),
                            Card10_4 = reader.GetUInt16("Card10_4"),
                            LastRewardEXP = reader.GetUInt16("LastRewardEXP"),
                            LastRewardCredits = reader.GetUInt16("LastRewardCredits")
                        };
                        quests.Add(quest.PlayerID, quest);
                    }
                }
                Logger.Info("[QuestsTable] with {0} players", quests.Count);
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }

        public static void UpdateCard(ulong PlayerID, int CardID)
        {
            try
            {
                MySqlHelper.ExecuteNonQuery(Connector.ConnectionString, "UPDATE players_quests SET CardID='" + CardID + "' WHERE PlayerID='" + PlayerID + "';");

                Logger.Info("[QuestsTable] Player successfully update Card");
            }
            catch (Exception ex)
            {
                Logger.Error("[Error] {0}", ex);
            }
        }
    }
}
