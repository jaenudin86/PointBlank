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
using System.Net;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BASE_USER_ENTER_REQ : ReceivePacket
    {
        private string login;
        private int loginLength;
        public PROTOCOL_BASE_USER_ENTER_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            //ReadH();
            loginLength = ReadC();
            login = ReadS(loginLength);
            AccountTable.LoadTable();
            getClient().setAccount(AccountTable.accounts[login]);
            /* Загружаем всю базу данных крч */
            ClansTable.LoadTable();
            ItemsTable.LoadTable();
            PlayersTable.LoadTable();
            QuestsTable.LoadTable();
            TitlesTable.LoadTable();
            PlayersConfigTable.LoadTable();
            PlayersStatsTable.LoadTable();
            TitlesTable.LoadTable();
            PlayerEquipTable.LoadTable();
            PlayersMedalsTable.LoadTable();
            getClient().setPlayer(PlayersTable.players[getClient().getAccount().AccountID]);
            getClient().getPlayer().setClient(getClient());
            getClient().getPlayer().setAddress(((IPEndPoint)getClient()._address).Address.ToString());
        }

        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_BASE_USER_ENTER_ACK());
        }
    }
}
