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

namespace Battle
{
    class Room
    {
        private int id;
        public IPAddress inetAddress; //host
        public int port;
        private Dictionary<int, IPAddress> players = new Dictionary<int, IPAddress>();

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public void setPlayers(Dictionary<int, IPAddress> players)
        {
            this.players = players;
        }

        public Dictionary<int, IPAddress> getPlayers()
        {
            return players;
        }

        public virtual void RemovePlayer(int ip)
        {
            for (int i = 0; i < getPlayers().Count; i++)
            {
                if (getPlayers() != null)
                {
                    players.Remove(ip);
                }
            }
        }


    }
}
