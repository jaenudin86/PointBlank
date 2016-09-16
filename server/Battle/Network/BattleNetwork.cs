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
using System.Threading;
using System.Net;
using Battle.Managers;

namespace Battle.Network
{
    class BattleNetwork
    {
        private UdpClient _udpclient;
        private byte[] _buffer;
        private IPEndPoint remoteip = null;

        public BattleNetwork(UdpClient udp)
        {
            _udpclient = udp;
            //new Thread(read).Start();
        }

        public void close()
        {
            NetworkManager.Load().removeClient(this);
        }
    }
}
