/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Game.Managers;
using Game.Network.ServerPacket;
using Game.Network.ClientPacket;
using Core.Model;
using Core.Database.Tables;
using Game.Network.ClientPacket.BATTLE;
using Game.Network.ClientPacket.LOBBY;
using Game.Network.ClientPacket.ROOM;
using Game.Network.ClientPacket.FRIENDS;

namespace Game.Network
{
    public class GameNetwork
    {
        public EndPoint _address;
        public TcpClient _client;
        public NetworkStream _stream;
        private byte[] _buffer;
        private string login;
        private Player player;
        private Account account;

        public void setLogin(string lo) 
        { 
            login = lo; 
        }
        public string getLogin() 
        { 
            return login; 
        }

        public void setPlayer(Player player)
        {
            this.player = player;
        }

        public Player getPlayer()
        {
            return player;
        }

        public void setAccount(Account account)
        {
            this.account = account;
        }

        public Account getAccount()
        {
            return account;
        }



        public int getCryptKey() { return 29890; }
        public int getID() { return 5404; }
        public void setShift(int key) { CRYPT_KEY = key; }
        public int getShift() { return CRYPT_KEY; }

        public GameNetwork(TcpClient tcpClient)
        {
            Logger.Info("Client connected");
            _client = tcpClient;
            _stream = tcpClient.GetStream();
            _address = tcpClient.Client.RemoteEndPoint;
            new Thread(init).Start();
            new Thread(ReadImpl).Start();
        }

        public void init() 
        {
            SendPacket(new PROTOCOL_GAME_CHANNEL_LIST()); // TODO: Содержимое
        }

        public void SendPacket(SendPacket bp)
        {
            bp.WriteImpl();
            byte[] data = bp.ToByteArray();
            Int16 size = Convert.ToInt16(data.Length - 2);
            List<byte> list = new List<byte>(data.Length + 2);
            list.AddRange(BitConverter.GetBytes(size));
            list.AddRange(data);
            byte[] bytes = list.ToArray();
            byte[] opcode = new byte[] { bytes[2], bytes[3] };
            if (bytes.Length <= 0)
                return;
            _stream.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(EndSendCallBackStatic), 0);
        }

        public void EndSendCallBackStatic(IAsyncResult result)
        {
            _stream.EndWrite(result);
            _stream.Flush();
        }

        public void ReadImpl()
        {
            try
            {
                if (_stream == null || !_stream.CanRead)
                    return;
                _buffer = new byte[2];
                _stream.BeginRead(_buffer, 0, 2, new AsyncCallback(OnReceiveCallbackStatic), 0);
            }
            catch
            {
                close();
            }
        }

        private void OnReceiveCallbackStatic(IAsyncResult result)
        {
            int rs = 0;
            try
            {
                rs = _stream.EndRead(result);
                if (rs > 0)
                {
                    byte Length = _buffer[0];
                    if (_stream.DataAvailable)
                    {
                        _buffer = new byte[Length + 2];
                        _stream.BeginRead(_buffer, 0, Length + 2, new AsyncCallback(OnReceiveCallback), result.AsyncState);
                    }
                }
            }
            catch
            {
                close();
            }
        }

        public byte[] decryptC(byte[] data, int length)
        {
            int clientId = getID();
            int cryptKey = getCryptKey();
            int shift = getShift();
            if (shift <= 0)
            {
                shift = ((clientId + cryptKey) % 7) + 1;
                setShift(shift);
            }
            byte[] bytes = data;
            byte[] newb = new byte[data.Length];
            Array.Copy(bytes, 0, newb, 0, newb.Length);
            bytes = decrypt(newb, shift);
            return bytes;
        }

        public static byte[] decrypt(byte[] data, int shift)
        {
            byte lastElement = data[data.Length - 1];
            for (int i = data.Length - 1; i > 0; i--)
            {
                data[i] = (byte)(((data[i - 1] & 0xFF) << (8 - shift)) | ((data[i] & 0xFF) >> shift));
            }
            data[0] = (byte)((lastElement << (8 - shift)) | ((data[0] & 0xFF) >> shift));
            return data;
        }

        public void close() 
        { 
            NetworkManager.Load().removeClient(this); 
        }

        private void OnReceiveCallback(IAsyncResult result)
        {
            _stream.EndRead(result);
            byte[] buff = new byte[_buffer.Length];
            _buffer.CopyTo(buff, 0);
            if (buff.Length >= 2)
            {
                buff = decryptC(buff, buff.Length);
                handlePacket(buff);
            }
            new Thread(ReadImpl).Start();
        }

        private void handlePacket(byte[] buff)
        {
            byte[] opcode1 = new byte[] { buff[0], buff[1] };
            UInt16 opcode = BitConverter.ToUInt16(opcode1, 0);
            string hex = BitConverter.ToString(buff).Replace("-", " ");
            List<ReceivePacket> packets = new List<ReceivePacket>();
            switch (opcode)
            {
                case 0: break;
                //
                case 2579: packets.Add(new PROTOCOL_BASE_USER_ENTER_REQ(this, buff)); break;
                case 2571: packets.Add(new PROTOCOL_BASE_ENTER_CHANNELSELECT_REQ(this, buff)); break;
                case 2654: packets.Add(new PROTOCOL_BASE_PLAYER_EXIT_REQ(this, buff)); break;
                case 3087: packets.Add(new PROTOCOL_LOBBY_GET_ROOMINFO_REQ(this, buff)); break;
                //ЛОББИ
                case 2573: packets.Add(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_REQ(this, buff)); break;
                case 3079: packets.Add(new PROTOCOL_LOBBY_ENTER_REQ(this, buff)); break;
                case 3073: packets.Add(new PROTOCOL_LOBBY_GET_ROOMLIST_REQ(this, buff)); break;
                case 3089: packets.Add(new PROTOCOL_LOBBY_CREATE_ROOM_REQ(this, buff)); break;
                case 3081: packets.Add(new PROTOCOL_LOBBY_JOIN_ROOM_REQ(this, buff)); break;
                case 3102: packets.Add(new PROTOCOL_LOBBY_CREATE_PLAYER_REQ(this, buff)); break;
                case 3083: packets.Add(new PROTOCOL_LOBBY_LEAVE_REQ(this, buff)); break;
                case 3077: packets.Add(new PROTOCOL_LOBBY_QUICKJOIN_REQ(this, buff)); break;
                //ЧАТ
                case 2627: packets.Add(new PROTOCOL_CHAT_NORMAL_REQ(this, buff)); break;
                //ДРУЗЬЯ
                case 2591: packets.Add(new PROTOCOL_FRIEND_INFO_REQ(this, buff)); break;
                //КОМНАТА
                case 3849: packets.Add(new PROTOCOL_ROOM_CLOSE_SLOT_REQ(this, buff)); break;
                case 3845: packets.Add(new PROTOCOL_ROOM_CHANGE_TEAM_REQ(this, buff)); break;
                case 3886: packets.Add(new PROTOCOL_ROOM_CHANGE_INFO_REQ(this, buff)); break;
                case 3854: packets.Add(new PROTOCOL_ROOM_LOBBY_USER_LIST_REQ(this, buff)); break;
                case 3906: packets.Add(new PROTOCOL_ROOM_CHANGE_PASSWORD_REQ(this, buff)); break;
                case 3841: packets.Add(new PROTOCOL_ROOM_GET_PLAYER_INFO_REQ(this, buff)); break;
                //ИНВЕНТАРЬ
                case 3585: packets.Add(new PROTOCOL_INVENTORY_ENTER_REQ(this, buff)); break;
                case 3589: packets.Add(new PROTOCOL_INVENTORY_LEAVE_REQ(this, buff)); break;
                case 534: // Even 536
                case 536: packets.Add(new PROTOCOL_INVENTORY_USE_ITEM_REQ(this, buff)); break; // need for coupons marks and names.
                //case 542: packets.Add(new PROTOCOL_INVENTORY_DELETE_ITEM_REQ(this, buff)); break;
                //МАГАЗИН
                case 2821: packets.Add(new PROTOCOL_SHOP_LIST_REQ(this, buff)); break;
                case 2819: packets.Add(new PROTOCOL_SHOP_ENTER_REQ(this, buff)); break;
                case 530: packets.Add(new PROTOCOL_SHOP_BUY_ITEM_REQ(this, buff)); break;
                case 2817: packets.Add(new PROTOCOL_SHOP_LEAVE_REQ(this, buff)); break;
                //БОЙ
                case 3331: packets.Add(new PROTOCOL_BATTLE_READYBATTLE_REQ(this, buff)); break;
                case 3348: packets.Add(new PROTOCOL_BATTLE_PRESTARTBATTLE_REQ(this, buff)); break;
                case 3333: packets.Add(new PROTOCOL_BATTLE_STARTBATTLE_REQ(this, buff)); break;
                case 3356: packets.Add(new PROTOCOL_BATTLE_BOMB_TAB_REQ(this, buff)); break;
                case 3358: packets.Add(new PROTOCOL_BATTLE_BOMB_UNTAB_REQ(this, buff)); break;
                case 3372: packets.Add(new PROTOCOL_BATTLE_TIMER_SYNC_REQ(this, buff)); break;
                case 3650: packets.Add(new PROTOCOL_BATTLE_EQUIPMENT_INFO_REQ(this, buff)); break;
                case 3344: packets.Add(new opcode_3860_REQ(this, buff)); break;
                case 3337: packets.Add(new PROTOCOL_BATTLE_RESPAWN_REQ(this, buff)); break;
                case 3378: packets.Add(new PROTOCOL_BATTLE_BOT_RESPAWN_REQ(this, buff)); break;
                case 3354: packets.Add(new PROTOCOL_BATTLE_FRAG_INFO_REQ(this, buff)); break;
                case 3376: packets.Add(new PROTOCOL_BATTLE_BOT_CHANGELEVEL_REQ(this, buff)); break;
                case 3368: packets.Add(new PROTOCOL_BATTLE_DAMAGE_SABOTAGE_REQ(this, buff)); break;
                case 3394: packets.Add(new PROTOCOL_TUTORIAL_END_REQ(this, buff)); break;
                case 3384: packets.Add(new PROTOCOL_BATTLE_LEAVE_REQ(this, buff)); break;
                //ПЕРКИ|МИССИИ
                case 3862: packets.Add(new PROTOCOL_BASE_ENTER_PROFILE_REQ(this, buff)); break;
                case 3864: packets.Add(new PROTOCOL_BASE_LEAVE_PROFILE_REQ(this, buff)); break;
                case 2605: packets.Add(new PROTOCOL_BASE_MISSION_BUY_REQ(this, buff)); break;
                case 2601: packets.Add(new PROTOCOL_BASE_MISSION_UPDATE_CARD_REQ(this, buff)); break;
                //КЛАНЫ
                case 0x5A1: packets.Add(new PROTOCOL_CLAN_ENTER_REQ(this, buff)); break;
                case 0x5A3: packets.Add(new PROTOCOL_CLAN_LEAVE_REQ(this, buff)); break;
                case 0x5A5: packets.Add(new PROTOCOL_CLAN_LIST_REQ(this, buff)); break;
                case 0x5AB: packets.Add(new PROTOCOL_CLAN_LIST_REQ(this, buff)); break;
                case 0x588: packets.Add(new PROTOCOL_CLAN_REQUESITES_FOR_CREATE_REQ(this, buff)); break;
                case 0x5A7: packets.Add(new PROTOCOL_CLAN_CHECK_NAME_REQ(this, buff)); break;
                case 0x51E: packets.Add(new PROTOCOL_CLAN_CREATE_REQ(this, buff)); break;
                case 0x518: packets.Add(new PROTOCOL_CLAN_INFO_REQ(this, buff)); break;
                case 0x51A: packets.Add(new PROTOCOL_CLAN_MEMBER_LIST_REQ(this, buff)); break;
                case 0x51C: packets.Add(new PROTOCOL_CLAN_MEMBER_LIST2_REQ(this, buff)); break;
                //ГОЛОСОВАНИЕ В ИГРЕ
                case 3396: packets.Add(new PROTOCOL_VOTEKICK_START_REQ(this, buff)); break;
                //НАСТРОЙКИ
                case 2581: packets.Add(new PROTOCOL_SETTINGS_SAVE_REQ(this, buff)); break;

                default:
                {
                    Logger.Warn("[Packet Handler] Received unk request " + opcode);
                    Logger.Warn("{0} ",hex); 
                    break;
                }
            }
            if (packets != null && packets.ToArray().Length > 0)
            {
                foreach (ReceivePacket msg in packets)
                {
                    runNewThread(new Thread(new ThreadStart(msg.RunImpl)));
                }
            }
        }
        public static void runNewThread(Thread t) { t.Start(); }
        public int CRYPT_KEY { get; set; }
    }
}
