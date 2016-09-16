/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using PointBlank.Network;

namespace PointBlank.Manager
{
    class NetworkManager
    {
        private static NetworkManager _instance = new NetworkManager();
        private List<Auth> _loggedClients = new List<Auth>();
        private SortedList<string, Auth> _waitingAcc = new SortedList<string, Auth>();
        protected SortedList<string, DateTime> _flood = new SortedList<string, DateTime>();
        public static NetworkManager Load() { return _instance; }
        public void addClient(TcpClient client)
        {
            Auth lc = new Auth(client);
            if (_loggedClients.Contains(lc)) { return; }
            _loggedClients.Add(lc);
        }
        public void removeClient(Auth loginClient)
        {
            if (!_loggedClients.Contains(loginClient)) return;
            _loggedClients.Remove(loginClient);
        }
    }
}