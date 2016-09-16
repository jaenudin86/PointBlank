/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Model;
using Core.Config;
using Core.Database.Tables;
using Core.Data.Parsers;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_BATTLE_END_ACK : SendPacket
    {
        private Player player;
        private Room room;
        Clan clan;
        Levels level;
        private byte Ivent = 8;
        PlayerStats stats;

        public PROTOCOL_BATTLE_END_ACK(Player player, Room room)
        {
            this.player = player;
            this.room = room;
        }

        public override void WriteImpl()
        {

            SLOT slotByPlayer = room.getRoomSlotByPlayer(player);
            clan = player.Clan;

            level = LevelUpParser._levels[player.getRank()];
            stats = PlayersStatsTable.statistics[player.PlayerID];

            /* Левел ап */
            if(player.getRank() < 52)
            {
                if(player.getExp() >= level.getOnAllExp())
                {
                    player.setRank(player.getRank() + 1);
                    PlayersTable.UpdateRank(player.PlayerID, player.getRank());
                    player.setGp(player.getGp() + LevelUpParser._levels[player.getRank()].getOnGPUp());
                    PlayersTable.UpdateGP_Exp(player.PlayerID, player.getGp() + level.getOnGPUp(), player.getExp());

                    player.getClient().SendPacket(new PACKET_LEVEL_UP_ACK(player.getRank()));
                }
            }

            WriteH(0xD08);
            if (room.getType() == 1)
            {
                if (room.getBlueKills() > room.getRedKills()){WriteC(1);}
                if (room.getRedKills() > room.getBlueKills()){WriteC(0);}
                if (room.getRedKills() == room.getBlueKills()){WriteC(2);}
            }
            if (room.getType() == 2)
            {
                if (room.getBlueWinRounds() > room.getRedWinRounds()){WriteC(1);}
                if (room.getRedWinRounds() > room.getBlueWinRounds()){WriteC(0);}
                if (room.getRedWinRounds() == room.getBlueWinRounds()){WriteC(2);}
            }
            if (room.getType() == 4)
            {
                if (room.getBlueWinRounds() > room.getRedWinRounds()){WriteC(1);}
                if (room.getRedWinRounds() > room.getBlueWinRounds()){WriteC(0);}
                if (room.getRedWinRounds() == room.getBlueWinRounds()){WriteC(2);}
            }
            WriteH(3); // что это???
            WriteH(2); // что это???

            int GP, Exp;
            // Опыт
            for (int i = 0; i < 16; i++)
            {
                SLOT slot = room.getRoomSlot(i);
                if (room.getSpecial() == 6)
                {
                    WriteH((short)slot.getAllExp());
                    player.setExp(player.getExp() + slot.getAllExp());

                }
                else
                {
                    WriteH((short)slot.getAllExp());
                    player.setExp(player.getExp() + slot.getAllExp());
                }
            }

            // Очки
            for (int i = 0; i < 16; i++)
            {
                SLOT slot = room.getRoomSlot(i);
                if (room.getSpecial() == 6)
                {
                    WriteH((short)slot.getAllGp());
                    player.setExp(player.getExp() + slot.getAllExp());
                }
                else
                {
                    WriteH((short)slot.getAllGp());
                    player.setExp(player.getExp() + slot.getAllExp());
                }
            }

            /* Записываем в базу данных */
            Exp = player.getExp();
            GP = player.getGp();
            PlayersTable.UpdateGP_Exp(player.PlayerID, GP, Exp);

            //Очки за ботов
             for (int i = 0; i < 16; i++)
             {
                 WriteH(0);
             }

             WriteB(new byte[]{
                 //Рейты по 2 байта на слот
           //В скобках|Иконка
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
                 0x08, 0x00,
             });
             WriteB(new byte[]{
                 //Иконки Ivent/PC_Cafe/Item
                 0xFF, 0x00,
                 0xFF, 0x00,
                 0xFF, 0x00,
                 0xFF, 0x00,
                 0xFF, 0x00,
                 0xFF, 0x00,
                 0xFF, 0x00,
                 0xFF, 0x00,
             });

             WriteS(player.getName(), Player.MAX_NAME_SIZE); // Имя перса
             WriteD(player.getExp()); // опыт
             WriteD(player.getRank()); // ранк (0-54)
             WriteD(player.getRank()); // фейк-ранг для купона скрытия ранга
             WriteD(player.getGp()); // ГП
             WriteD(player.getMoney()); // Рублики

             WriteD(player == null ? (int)clan.Id : (int)0); // ClanID
             WriteD(player == null ? (int)clan.Color : (int)0); // ClanNameColor
             
             WriteD(0); // Unk
             WriteC(0); // Unk
             WriteD(player.getPCCafe());//pc cafe
             WriteH((short)player.getEmblem()); //Лычка поидеи

             WriteS("", 17);
             WriteC(0);//unk

             WriteH(clan != null ? (short)clan.Rank : (short)0);
             WriteC(Convert.ToByte(((this.player == null) || (clan == null)) ? 0xff : clan.getLogo1()));
             WriteC(Convert.ToByte(((this.player == null) || (clan == null)) ? 0xff : clan.getLogo2()));
             WriteC(Convert.ToByte(((this.player == null) || (clan == null)) ? 0xff : clan.getLogo3()));
             WriteC(Convert.ToByte(((this.player == null) || (clan == null)) ? 0xff : clan.getLogo4()));
             WriteH(0);

             WriteD(0); // Непонятно разделитель

             WriteB(new byte[8] { 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01});

             /* Статистика */
             WriteD(stats.getFights());//всего боев 
             WriteD(stats.getWins());//всего побед
             WriteD(stats.getLosts());//всего поражений
             WriteD(0); // unk
             WriteD(stats.getKills());//кол-во убийств
             WriteD(stats.getHeadshots());//кол-во хедшотов
             WriteD(stats.getDeaths());//кол-во смертей
             WriteD(0); // unk
             WriteD(stats.getKills());//опять килы о.о
             WriteD(stats.getEscapes());//всего ливнул
             WriteD(stats.getSeasonFights());//всего боев за сезон
             WriteD(stats.getSeasonWins());//всего побед за сезон
             WriteD(stats.getSeasonLosts());//всего поражений за сезон
             WriteD(0); // unk
             WriteD(stats.getSeasonKills());//киллы сезон по идее
             WriteD(stats.getSeasonHeadshots());//хеды сезон по идее
             WriteD(stats.getSeasonDeaths());//смерти сезон по идее
             WriteD(0); // unk
             WriteD(stats.getSeasonKills());//опять килы,хз зачем
             WriteD(stats.getSeasonEscapes());//сколько ливнул за сезон

             WriteH((short)room.getRedWinRounds());
             WriteH((short)room.getBlueWinRounds());
             WriteB(new byte[49]);
        }
    }
}
