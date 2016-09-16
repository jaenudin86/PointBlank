using Core.Database;
using Core.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Core.Database.Tables
{
    public class MessagesTable : Connector
    {
        public static Dictionary<string, List<Messages>> messageslist;

        public static void LoadTable()
        {
            try
            {
                messageslist = new Dictionary<string, List<Messages>>();
                using (MySqlDataReader mySqlDataReader = MySqlHelper.ExecuteReader(Connector.ConnectionString, "SELECT * FROM `players_messages`"))
                {
                    while (mySqlDataReader.Read())
                    {
                        Messages messages = new Messages()
                        {
                            owner_id = mySqlDataReader.GetInt32("PlayerId"),
                            object_id = mySqlDataReader.GetInt32("OwnerId"),
                            author_name = mySqlDataReader.GetString("RecipientName"),
                            message = mySqlDataReader.GetString("Text")
                        };
                        if (!messageslist.ContainsKey(messages.author_name))
                        {

                            messageslist.Add(messages.author_name, new List<Messages>());

                        }
                    }
                }
                Core.Logger.Info("[MessagesTable] Loaded {0} messages", messageslist.Count);
            }
            catch (Exception ex)
            {
                Core.Logger.Error("[Error] {0}", (object)ex);
            }
        }
    }
}
