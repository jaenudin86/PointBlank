/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Net.Sockets;
using System.IO;
using Core.Model;
using Core.Config;

namespace Game.Network
{
    public class BattleHandler
    {
        private static UdpClient client = new UdpClient(40001);

        public static void CreateRoom(int id, Player p)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.WriteByte(1);
            memoryStream.WriteByte((byte)id);
            memoryStream.Write(p.getAddress(), 0, 4);
            client.Send(memoryStream.ToArray(), memoryStream.ToArray().Length, ConfigModel.BATTLE_SERVER , 40000);
        }

        public static void AddPlayer(Player p)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.WriteByte(2);
            memoryStream.WriteByte((byte)p.getRoom().getId());
            memoryStream.Write(p.getAddress(), 0, 4);
            client.Send(memoryStream.ToArray(), memoryStream.ToArray().Length, ConfigModel.BATTLE_SERVER, 40000);
        }


        public static void DeleteRoom(Room room)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.WriteByte(17);
            memoryStream.WriteByte((byte)room.getId());
            client.Send(memoryStream.ToArray(), memoryStream.ToArray().Length, ConfigModel.BATTLE_SERVER, 40000);
        }

        public static void ChangeHost(Room room, Player p)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.WriteByte(18);//опкод
            memoryStream.WriteByte((byte)room.getId());
            memoryStream.Write(p.getAddress(), 0, 4);//ip адрес нового лидера комнаты
            client.Send(memoryStream.ToArray(), memoryStream.ToArray().Length, "25.117.204.60", 40000);
        }
    }
}
