/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network.ServerPacket;
using Core.Database.Tables;
using Core.Model;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_LOBBY_CREATE_PLAYER_REQ : ReceivePacket
    {
        private string Name;
        private byte NameLenght;
        Player checkPlayer;

        public PROTOCOL_LOBBY_CREATE_PLAYER_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            NameLenght = ReadC();
            Name = ReadS((int)NameLenght - 1);
        }
        public override void RunImpl()
        {
            checkPlayer = PlayersTable.checkPlayer[Name];
            if (checkPlayer == null)
            {
                PlayersTable.CreatePlayer(getClient().getAccount().AccountID, Name);
                getClient().SendPacket(new PROTOCOL_LOBBY_CREATE_PLAYER_ACK(0x4a100080));
            }
            else
            {
                getClient().SendPacket(new PROTOCOL_LOBBY_CREATE_PLAYER_ACK(0x80000113));//занято!
            }
        }
    }
}
