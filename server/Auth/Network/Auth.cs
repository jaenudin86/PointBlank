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
using PointBlank.Manager;
using PointBlank.Network.ServerPacket;
using PointBlank.Network.ClientPacket;
using Core.Model;
using Core.Protection;
using Core.Model.Protection;
using Core;

namespace PointBlank.Network
{
    public class Auth
    {
        public EndPoint _address;
        public TcpClient _client;
        public NetworkStream _stream;
        private byte[] _buffer;
        private string login;
        private Account account;

        public SocketAddress getAddress()
        {
            return _client.Client.RemoteEndPoint.Serialize();
        }

        public void setLogin(string lo)
        {
            login = lo;
        }
        public string getLogin()
        {
            return login;
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

        public Auth(TcpClient tcpClient)
        {
            try
            {
                _client = tcpClient;

                /* Protection */
                string IPv4 = Convert.ToString(
                    _client.Client.RemoteEndPoint.Serialize()[4] + "." +
                    _client.Client.RemoteEndPoint.Serialize()[5] + "." +
                    _client.Client.RemoteEndPoint.Serialize()[6] + "." +
                    _client.Client.RemoteEndPoint.Serialize()[7]
                );

                ProtectionService.addAttack(IPv4);

                if (ProtectionService.attackers.ContainsKey(IPv4))
                {
                    if(ProtectionService.attackers[IPv4].Status == true)
                    {
                        if(ProtectionService.attackers[IPv4].Connect == false)
                        {
                            ProtectionService.attackers[IPv4].Connect = true;
                            _client.Close();
                            Logger.Protection("[Protection] Client " + IPv4 + " aborted.");
                        }
                    }
                    else
                    {
                        _stream = tcpClient.GetStream();
                        _address = tcpClient.Client.RemoteEndPoint;
                        new Thread(init).Start();
                        new Thread(ReadImpl).Start();
                        Logger.Info("[Network] Client " + IPv4 + " connected.");
                    }
                }
            }
            catch(Exception e)
            {
                Logger.Error("[Network] Error: {0}", e);
            }
        }

        public void init()
        {
            SendPacket(new PROTOCOL_LOGIN_CHANNEL_LIST()); // TODO: Содержимое
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

            //Array.Reverse(bytes, 0, bytes.Length);

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
                case 2561: packets.Add(new PROTOCOL_BASE_LOGIN_WEBKEY_RUSSIA_REQ(this, buff)); break;
                case 2565: packets.Add(new PROTOCOL_BASE_GET_MYINFO_REQ(this, buff)); break;
                case 2577: packets.Add(new PROTOCOL_BASE_USER_LEAVE_REQ(this, buff)); break;
                case 2666: packets.Add(new opcode_2667_REQ(this, buff)); break;
                case 2678: packets.Add(new opcode_2678_REQ(this, buff)); break;
                case 2567: packets.Add(new PROTOCOL_AUTH_FRIEND_INFO_REQ(this, buff)); break;
                default: Logger.Warn("[Packet Handler] Received unk request " + opcode); break;
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

