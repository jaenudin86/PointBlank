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
using System.IO;
using System.Net;
using System.Net.Sockets;
using Battle.Managers;

namespace Battle
{
    class NetworkS
    {
        private static NetworkS _instance;
        private static UdpClient _client;
        private static List<IPAddress> players;

        public static NetworkS Load()
        {
            _instance = new NetworkS();
            return _instance;
        }

        public NetworkS()
        {
            UdpClient udp = new UdpClient(40000);
            Logger.Info("[Network] Battle Server Host {0}", IPAddress.Any);
            _client = default(UdpClient);
            while(true)
            {
                //Thread.Sleep(100);
                if (udp != null)
                {
                    if (udp.Available > 0) //TODO:: Выбивает исключение, нужно проверять как то по другому....
                    {
                        _client = udp;

                        _client.BeginReceive(new AsyncCallback(BeginReceive), null);
                    }
                }
            }
        }
        private static void BeginReceive(IAsyncResult result)
        {
            IPEndPoint remoteEP = null;
            MemoryStream stream2;
            MemoryStream stream = new MemoryStream();
            byte[] buffer = _client.EndReceive(result, ref remoteEP);
            byte[] bytee = new byte[] { 0x04, 0xff, 0x91, 0xed, 0x0b, 0x41, 0x01, 0x52, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x17, 0x00, 0x00, 0x0a, 0x00, 0x00, 0xe6, 0xce, 0xc1, 0x44, 0x9a, 0xbe, 0xef, 0x55, 0xb3, 0x43, 0x05, 0x00, 0xf0, 0x1f, 0x09, 0x0a, 0x00, 0x06, 0x00, 0x00, 0x09, 0x0b, 0x00, 0x06, 0x00, 0x00, 0x09, 0x0c, 0x00, 0x06, 0x00, 0x00, 0x09, 0x0d, 0x00, 0x06, 0x00, 0x00, 0x09, 0x0e, 0x00, 0x06, 0x00, 0x00 };
            byte[] bytee2 = new byte[] { 0x42, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1d, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            switch (buffer[0])
            {
               // case 4:
                case 1:
                    {
                        Room room = new Room();
                        room.setId(buffer[1]);
                        String ip = buffer[2].ToString() + "." + buffer[3].ToString() + "." + buffer[4].ToString() + "." + buffer[5].ToString();
                        room.inetAddress = IPAddress.Parse(ip);
                        RoomManager.rooms.Add(RoomManager.rooms.Count, room);
                        Logger.Info("New room");
                        Logger.Info("{0}", RoomManager.rooms.Count);
                        break;
                    }
                case 2:
                    {

                        String ip = buffer[2].ToString() + "." + buffer[3].ToString() + "." + buffer[4].ToString() + "." + buffer[5].ToString();
                        Logger.Info("New player {0}", ip);
                        Room room = RoomManager.rooms[buffer[1]];
                        if (room != null)
                        {
                            if (!room.getPlayers().Equals(ip))
                            {
                                room.getPlayers().Add(room.getPlayers().Count, IPAddress.Parse(ip));
                            }
                        }
                        break;
                    }
                case 17:
                    RoomManager.removeRoom(buffer[1]);
                    Logger.Info("Room " + buffer[1] + " remove.");
                    return;

                case 18:
                    String NewIP = buffer[2].ToString() + "." + buffer[3].ToString() + "." + buffer[4].ToString() + "." + buffer[5].ToString();
                    Room NewRoom = RoomManager.rooms[buffer[1]];
                    if (NewRoom != null)
                    {
                        if (!NewRoom.getPlayers().Equals(NewIP))
                        {
                            NewRoom.getPlayers().Add(NewRoom.getPlayers().Count, IPAddress.Parse(NewIP));
                        }
                    }
                    
                    return;

                case 65:
                    {
                        stream2 = new MemoryStream();
                        stream2.WriteByte(66);
                        stream2.WriteByte(0);
                        stream2.Write(new byte[5], 0, 5);
                        stream2.WriteByte(0x0b);
                        stream2.Write(new byte[3], 0, 3);
                        // stream2.Write(buffer, 1, buffer.Length - 1);
                        _client.Send(stream2.ToArray(), stream2.ToArray().Length, remoteEP);
                        break;
                    }
                case 67:
                    new Player()._address = remoteEP.Address;
                    Logger.Info("Player remove");
                    //Program.getRoomManager().RemovePlayerInRoom(remoteEP.Address);
                    return;
                case 131:
                case 132:
                case 84:
                case 97:
                case 4:
                case 3:
                    {
                        Room room = RoomManager.getRoom(remoteEP.Address);
                        if (room.inetAddress.Equals(remoteEP.Address))
                        {
                            foreach (IPAddress player in room.getPlayers().Values)
                            {
                                stream2 = new MemoryStream();
                                stream2.WriteByte(buffer[0]);
                                // stream2.WriteByte(0);
                                stream2.Write(buffer, 1, buffer.Length - 1);
                                _client.Send(stream2.ToArray(), stream2.ToArray().Length, player.ToString(), 29890);
                            }
                        }
                        else
                        {
                            stream2 = new MemoryStream();
                            stream2.WriteByte(buffer[0]);
                            //stream2.WriteByte(0);
                            stream2.Write(buffer, 1, buffer.Length - 1);
                            _client.Send(stream2.ToArray(), stream2.ToArray().Length, room.inetAddress.ToString(), 29890);
                        }
                        /* stream2 = new MemoryStream();
                         stream2.WriteByte(4);
                         stream2.WriteByte(buffer[1]);
                         stream2.Write(buffer, 2, buffer.Length - 2);
                         _client.Send(stream2.ToArray(), stream2.ToArray().Length, "192.168.0.101", 29890);*/
                        break;
                    }
                default:
                    {
                        Logger.Info("New packet {0}",buffer[0]);
                        string hex = BitConverter.ToString(buffer).Replace("-", " ");
                        Logger.Info("{0}", hex.ToString());
                        break;
                    }
            }
        }
    }
}
