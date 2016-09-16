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
using Game.Network;

namespace Game.Managers
{
    class NetworkManager
    {
        private static NetworkManager _instance = new NetworkManager();
        private List<GameNetwork> _loggedClients = new List<GameNetwork>();
        private SortedList<string, GameNetwork> _waitingAcc = new SortedList<string, GameNetwork>();
        protected SortedList<string, DateTime> _flood = new SortedList<string, DateTime>();
        public static NetworkManager Load() { return _instance; }
        public void addClient(TcpClient client)
        {
            GameNetwork lc = new GameNetwork(client);
            if (_loggedClients.Contains(lc)) { return; }
            _loggedClients.Add(lc);
        }
        public void removeClient(GameNetwork loginClient)
        {
            if (!_loggedClients.Contains(loginClient)) return;
            _loggedClients.Remove(loginClient);
        }
    }
}
