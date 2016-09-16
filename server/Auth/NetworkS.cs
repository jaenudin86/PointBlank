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
using System.Threading;
using Core.Config;
using PointBlank.Manager;
using Core;

namespace PointBlank
{
    class NetworkS
    {
        private static NetworkS _instance;
        private static TcpListener _clientLoginListener;
        private static TcpClient _client;

        public static NetworkS Load()
        {
            _instance = new NetworkS(); 
            return _instance;
        }

        public NetworkS()
        {
            new Thread(Start).Start();
        }

        public void Start()
        {
            _clientLoginListener = new TcpListener(IPAddress.Parse(ConfigModel.HOST), ConfigModel.PORT);
            _clientLoginListener.Start();
            _client = default(TcpClient);
            Logger.Info("[Network] Auth Server Host {0}",ConfigModel.HOST);
            while (true)
            {
                _client = _clientLoginListener.AcceptTcpClient();
                accept(_client);
                Thread.Sleep(1);
            }
        }
        private static void accept(TcpClient client) 
        { 
            NetworkManager.Load().addClient(client); 
        }
    }
}
