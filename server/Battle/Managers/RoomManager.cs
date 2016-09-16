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

namespace Battle.Managers
{
    class RoomManager
    {
        public static Dictionary<int, Room> rooms = new Dictionary<int, Room>();


        public static void AddRoom(Room room)
        {
            rooms.Add(room.getId() , room);
        }

        public static Room getRoom(int id)
        {
            return rooms[id];
        }

        public static IPAddress getPlayer(IPAddress ip)
        {
            foreach (Room room in rooms.Values)
            {
                foreach (IPAddress player in room.getPlayers().Values)
                {
                    if (player.Equals(ip))
                    {
                        return player;
                    }
                }
            }
            return null;
        }

        public static Room getRoom(IPAddress ip)
        {
            foreach (Room room in rooms.Values)
            {
                foreach (IPAddress player in room.getPlayers().Values)
                {
                    if (player.Equals(ip))
                    {
                        return room;
                    }
                }
                if (room.inetAddress.Equals(ip))
                {
                    return room;
                }
            }
            return null;
        }

        public static Dictionary<int, IPAddress> getPlayers(IPAddress ip)
        {
            foreach (Room room in rooms.Values)
            {
                foreach (IPAddress player in room.getPlayers().Values)
                {
                    if (player.Equals(ip))
                    {
                        return room.getPlayers();
                    }
                }
            }
            return null;
        }

        public static void removeRoom(int id)
        {
            rooms.Remove(id);
        }
    }
}
