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
using Core.Model;
using Game.Managers;
using Core.Database.Tables;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BASE_MISSION_BUY_REQ : ReceivePacket
    {
        private int missionID;
        private int GP;
        private int Money;

        public PROTOCOL_BASE_MISSION_BUY_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            //ReadD();
            missionID = ReadC();
        }

        public override void RunImpl()
        {
            if (getClient() == null)
                return;
            Player player = getClient().getPlayer();
            //PlayersTable.UpdateMission(player.PlayerID, missionID, 0);
            switch (missionID)
            {
                case 1://Tutorial Mission Card
                    break;

                case 2://Dino Tutorial Mission Card
                    break;

                case 3://Infantry Card Set
                    break;

                case 5://
                    player.setGp(player.getGp() - 5000);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;

                case 6://
                    player.setGp(player.getGp() - 5000);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;

                case 7://
                    player.setGp(player.getGp() - 5000);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;

                case 8://
                    player.setGp(player.getGp() - 5400);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;

                case 9://
                    player.setGp(player.getGp() - 5800);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;

                case 10://
                    player.setGp(player.getGp() - 8300);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;

                case 11://
                    player.setGp(player.getGp() - 11000);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;

                case 12://
                    break;

                case 14://
                    player.setGp(player.getGp() - 5500);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;

                case 15://
                    player.setGp(player.getGp() - 5000);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;

                case 16:
                    player.setGp(player.getGp() - 9500);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;

                case 17:
                    player.setGp(player.getGp() - 9000);//вычитаем цену сета
                    Money = player.getGp();
                    GP = player.getMoney();
                    //Обновляем кол-во средств на аккаунте
                    PlayersTable.UpdateMoney(player.PlayerID, GP, Money);
                    break;
            }
            getClient().SendPacket(new PROTOCOL_BASE_MISSION_BUY_ACK(missionID, player));
        }
    }
}
