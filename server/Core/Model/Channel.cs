/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Database.Tables;
using Core.Managers;

namespace Core.Model
{
    public class Channel
    {
        private int id;
        public static int MAX_ROOMS_COUNT = 50;
        public static int MAX_PLAYERS_COUNT = 150;
        public int type;
        public string announce;
        private Dictionary<ulong, Player> players = new Dictionary<ulong, Player>();
        private Dictionary<int, Room> rooms = new Dictionary<int, Room>();

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getType()
        {
            return type;
        }

        public void setType(int type)
        {
            this.type = type;
        }

        public string getAnnounce()
        {
            return announce;
        }

        /* Удаление пустых комнат с канала | Нужно вызывать вместе с пакетом lobby_enter */
        public void RemoteEmptyRooms()
        {
            foreach(var room in getRooms().ToArray())
            {
                if(room.Value.getPlayers().Count == 0)
                {
                    removeRoom(room.Value);
                }
            }
        }

        

        public void setAnnounce(string announce)
        {
            this.announce = announce;
        }

        public Dictionary<ulong, Player> getPlayers()
        {
            return players;
        }

        public void addPlayer(Player player)
        {
            if (players.Count < MAX_PLAYERS_COUNT)
            {
                if (!players.ContainsKey(player.PlayerID))
                {
                    players.Add(player.PlayerID, player);
                }
            }
        }


        public Dictionary<int, Room> getRooms()
        {
            return rooms;
        }

        public void removeRoom(Room room)
        {
            rooms.Remove(room.getId());
        }

        public Room getRoom(int id)
        {
            return rooms[id];
        }

        public void addRoom(Room room)
        {
            if (rooms.Count < MAX_ROOMS_COUNT)
            {
                if (!rooms.ContainsValue(room))
                {
                    rooms.Add(room.getId(), room);
                }
            }
        }

        
        public void removePlayer(Player player)
        {
            players.Remove(player.PlayerID);
        }
        


        public Player getPlayerFromPlayerId(ulong PlayerID)
        {
            for (int i = 0; i < players.Count; ++i)
            {
                if (players[(ulong)i].PlayerID == PlayerID)
                    return PlayersTable.players[PlayerID];
            }
            return null;
        }

        /* в тестовом режиме */
        public List<Player> getWaitPlayers()
        {
            List<Player> list = new List<Player>();
            for(ulong i = 0; i < (ulong)players.Count; ++i)
            {
                if(players[i].getRoom() == null)
                {
                    list.Add(players[(ulong)i]);
                }
            }
            return list;
        }

        public void RemovePlayer(Player player)
        {
            for (int i = 0; i < players.Count; ++i)
            {
                if (player.PlayerID == players[(ulong)i].PlayerID)
                {
                    Player playerFromPlayerId = getPlayerFromPlayerId(players[(ulong)i].PlayerID);
                    playerFromPlayerId.ChannelId = -1;
                    players.Remove(player.PlayerID);
                }
            }
        }


        public void CreateRoom(Room room)
        {
            rooms.Add(room.getId(), room);
        }


        public Room getRoomInId(int id)
        {
            for (int i = 0; i < rooms.Count; ++i)
            {
                if (rooms[i].getId() == id)
                    return rooms[i];
            }
            return null;
        }
    }
}
