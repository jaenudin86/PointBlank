/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using Core.Config;
using Core.Database.Tables;

namespace Core.Database
{
    public class Connector
    {
        public static MySqlConnection mysqlconnect;
        public static String ConnectionString = "SERVER=" + IPAddress.Parse(ConfigModel.DB_IP) + ";PORT=" + ConfigModel.DB_PORT + ";DATABASE=" + ConfigModel.DB_NAME + ";UID=" + ConfigModel.DB_USER + ";PASSWORD=" + ConfigModel.DB_PASS + ";";
        public static bool Connect()
        {
            Connector.mysqlconnect = new MySqlConnection(ConnectionString);
            
            try
            {
                mysqlconnect.Open();
                Logger.Info("[Database] MySql Connect Host: "+ ConfigModel.DB_IP);
            }
            catch
            {
                Logger.Info("[Database] DataBase MySql (Cannot connect to server.)");
            }
            return mysqlconnect.State.ToString() == "Open";
        }

        public static MySqlConnection getConnect()
        {
            return mysqlconnect;
        }
        private bool close()
        {
            try
            {
                mysqlconnect.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }
    }
}
