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
using Battle.Network;

namespace Battle.Managers
{
    class NetworkManager
    {
        private static NetworkManager _instance = new NetworkManager();
        private List<BattleNetwork> _loggedClients = new List<BattleNetwork>();
        public static NetworkManager Load() { return _instance; }

        public List<BattleNetwork> getLoggedClients()
        {
            return _loggedClients;
        }

        public void addClient(UdpClient client)
        {
            BattleNetwork lc = new BattleNetwork(client);
            if (_loggedClients.Contains(lc))
            {
                //CLogger.info("Client is Have");
                return;
            }
            _loggedClients.Add(lc);
        }

        public void removeClient(BattleNetwork loginClient)
        {
            if (!_loggedClients.Contains(loginClient))
                return;
            _loggedClients.Remove(loginClient);
        }
    }
}
