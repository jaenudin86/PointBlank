/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using Core.Model;
using Core.Model.Missions;
using Core.Database.Tables;
using Core.Data.Parsers.Missions;
using Core.Unums;

namespace PointBlank.Network.ServerPacket
{
    class PROTOCOL_BASE_GET_MYINFO_ACK : SendPacket
    {
        Account account;
        Player player;
        Clan clan;
        PlayerStats stats;
        Titles titles;
        PlayerEquip equip;
        PlayerMedals medals;
        QuestCardSet quest;
        Tutorial tutorial;

        public PROTOCOL_BASE_GET_MYINFO_ACK(Account account)
        {
            this.account = account;
        }
        public override void WriteImpl()
        {
            player = PlayersTable.players[account.AccountID];
            stats = PlayersStatsTable.statistics[player.PlayerID];
            titles = TitlesTable.titles[player.PlayerID];
            equip = PlayerEquipTable.players_equip[player.PlayerID];
            medals = PlayersMedalsTable.playersMedals[player.PlayerID];
            quest = QuestsTable.quests[player.PlayerID];
            clan = player.Clan;

            WriteH(0xA06);
            WriteD(0);
            WriteC(0xdd);
            WriteS(player.PlayerName, 33);
            WriteD(player.getExp());
            WriteD(player.getRank());
            WriteD(player.getRank()); // Для купона
            WriteD(player.getGp());
            WriteD(player.getMoney());
            WriteD(player == null || clan == null ? 0 : (int)clan.getId());
            WriteD(player == null || clan == null ? 0 : clan.getColor());
            WriteD(0); // Unk
            WriteC(0); // Unk
            WriteD(player.getPCCafe());//pc cafe
            WriteH((short)player.getEmblem()); //Лычка поидеи
            WriteS(Convert.ToString(account == null || clan == null ? "" : clan.getName()), 16);
            WriteC((byte)0);
            WriteH(player == null || clan == null ? (short)0 : (short)clan.getRank());
            WriteC(Convert.ToByte(player == null || clan == null ? (int)byte.MaxValue : clan.getLogo1()));
            WriteC(Convert.ToByte(player == null || clan == null ? (int)byte.MaxValue : clan.getLogo2()));
            WriteC(Convert.ToByte(player == null || clan == null ? (int)byte.MaxValue : clan.getLogo3()));
            WriteC(Convert.ToByte(player == null || clan == null ? (int)byte.MaxValue : clan.getLogo4()));
            WriteH(player == null || clan == null ? (short)0 : (short)clan.getColor());
            WriteD(0); // unk
            WriteD(0);  // TODO посмотреть снифф
            WriteD(0);  // TODO посмотреть снифф

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
            LoadEquip();
            WriteB(new byte[41]);
            WriteC(1);
            LoadItems();
            WriteC(0);  // Outpost
            WriteD(medals.getRibbons());  // Медаль: Лента.
            WriteD(medals.getBadges());  // Медаль: Знаки отличия.
            WriteD(medals.getMedals());  // Медаль: сама медаль.
            WriteD(medals.getMaterMedals());  // Медаль: Медаль мастера.
            WriteC((byte)quest.getMissionID()); // Активная миссия?
            WriteD(quest.getCardID()); // Активная карта 

            tutorial = getMissionById(0);
            if (tutorial.getMission1() == quest.getCard1_1()) { if (tutorial.getMission2() == quest.getCard1_2()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission2() == quest.getCard1_2()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }
            if (tutorial.getMission3() == quest.getCard1_3()) { if (tutorial.getMission4() == quest.getCard1_4()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission4() == quest.getCard1_4()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }

            tutorial = getMissionById(1);
            if (tutorial.getMission1() == quest.getCard2_1()) { if (tutorial.getMission2() == quest.getCard2_2()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission2() == quest.getCard2_2()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }
            if (tutorial.getMission3() == quest.getCard2_3()) { if (tutorial.getMission4() == quest.getCard2_4()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission4() == quest.getCard2_4()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }

            tutorial = getMissionById(2);
            if (tutorial.getMission1() == quest.getCard1_1()) { if (tutorial.getMission2() == quest.getCard3_2()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission2() == quest.getCard3_2()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }
            if (tutorial.getMission3() == quest.getCard1_3()) { if (tutorial.getMission4() == quest.getCard3_4()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission4() == quest.getCard3_4()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }

            tutorial = getMissionById(3);
            if (tutorial.getMission1() == quest.getCard4_1()) { if (tutorial.getMission2() == quest.getCard4_2()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission2() == quest.getCard4_2()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }
            if (tutorial.getMission3() == quest.getCard4_3()) { if (tutorial.getMission4() == quest.getCard4_4()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission4() == quest.getCard4_4()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }

            tutorial = getMissionById(4);
            if (tutorial.getMission1() == quest.getCard5_1()) { if (tutorial.getMission2() == quest.getCard5_2()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission2() == quest.getCard5_2()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }
            if (tutorial.getMission3() == quest.getCard5_3()) { if (tutorial.getMission4() == quest.getCard5_4()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission4() == quest.getCard5_4()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }

            tutorial = getMissionById(5);
            if (tutorial.getMission1() == quest.getCard6_1()) { if (tutorial.getMission2() == quest.getCard6_2()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission2() == quest.getCard6_2()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }
            if (tutorial.getMission3() == quest.getCard6_3()) { if (tutorial.getMission4() == quest.getCard6_4()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission4() == quest.getCard6_4()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }

            tutorial = getMissionById(6);
            if (tutorial.getMission1() == quest.getCard7_1()) { if (tutorial.getMission2() == quest.getCard7_2()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission2() == quest.getCard7_2()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }
            if (tutorial.getMission3() == quest.getCard7_3()) { if (tutorial.getMission4() == quest.getCard7_4()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission4() == quest.getCard7_4()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }

            tutorial = getMissionById(7);
            if (tutorial.getMission1() == quest.getCard8_1()) { if (tutorial.getMission2() == quest.getCard8_2()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission2() == quest.getCard8_2()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }
            if (tutorial.getMission3() == quest.getCard8_3()) { if (tutorial.getMission4() == quest.getCard8_4()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission4() == quest.getCard8_4()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }

            tutorial = getMissionById(8);
            if (tutorial.getMission1() == quest.getCard9_1()) { if (tutorial.getMission2() == quest.getCard9_2()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission2() == quest.getCard9_2()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }
            if (tutorial.getMission3() == quest.getCard9_3()) { if (tutorial.getMission4() == quest.getCard9_4()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission4() == quest.getCard9_4()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }

            tutorial = getMissionById(9);
            if (tutorial.getMission1() == quest.getCard10_1()) { if (tutorial.getMission2() == quest.getCard10_2()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission2() == quest.getCard10_2()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }
            if (tutorial.getMission3() == quest.getCard10_3()) { if (tutorial.getMission4() == quest.getCard10_4()) { WriteC((byte)0xFF); } else { WriteC((byte)0xEF); } } else if (tutorial.getMission4() == quest.getCard10_4()) { WriteC((byte)0xFE); } else { WriteC((byte)0x00); }

            // Галки на миссиях: 0xEF - выполнена первая, 0xFE - выполнена вторая, 0xFF - выполнены обе, 0x00 - не выполнена не одна

            WriteB(new byte[]{
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,
            0x01, 0x00, 0x01, 0x00,}); //
            //прохождение карт
            WriteC((byte)quest.getCard1_1()); WriteC((byte)quest.getCard1_2()); WriteC((byte)quest.getCard1_3()); WriteC((byte)quest.getCard1_4());
            WriteC((byte)quest.getCard2_1()); WriteC((byte)quest.getCard2_2()); WriteC((byte)quest.getCard2_3()); WriteC((byte)quest.getCard2_4());
            WriteC((byte)quest.getCard3_1()); WriteC((byte)quest.getCard3_2()); WriteC((byte)quest.getCard3_3()); WriteC((byte)quest.getCard3_4());
            WriteC((byte)quest.getCard4_1()); WriteC((byte)quest.getCard4_2()); WriteC((byte)quest.getCard4_3()); WriteC((byte)quest.getCard4_4());
            WriteC((byte)quest.getCard5_1()); WriteC((byte)quest.getCard5_2()); WriteC((byte)quest.getCard5_3()); WriteC((byte)quest.getCard5_4());
            WriteC((byte)quest.getCard6_1()); WriteC((byte)quest.getCard6_2()); WriteC((byte)quest.getCard6_3()); WriteC((byte)quest.getCard6_4());
            WriteC((byte)quest.getCard7_1()); WriteC((byte)quest.getCard7_2()); WriteC((byte)quest.getCard7_3()); WriteC((byte)quest.getCard7_4());
            WriteC((byte)quest.getCard8_1()); WriteC((byte)quest.getCard8_2()); WriteC((byte)quest.getCard8_3()); WriteC((byte)quest.getCard8_4());
            WriteC((byte)quest.getCard9_1()); WriteC((byte)quest.getCard9_2()); WriteC((byte)quest.getCard9_3()); WriteC((byte)quest.getCard9_4());
            WriteC((byte)quest.getCard10_1()); WriteC((byte)quest.getCard10_2()); WriteC((byte)quest.getCard10_3()); WriteC((byte)quest.getCard10_4());

            WriteB(new byte[]{
				// Остальные миссии
                0x01, 0x01, 0x01, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
            });
            // ПЕРКИ
            //8 байт - открытые перки(маска) | система такая: у каждой перки свой айди, тут нужно складывать айди в стиле 1+2+3+4+5+6 где 1/2/3/4/5/6 есть айди перки | Всего 44 перки
            WriteB(new byte[8]
            {
                Convert.ToByte(titles.getP1()),
                Convert.ToByte(titles.getP2()),
                Convert.ToByte(titles.getP3()),
                Convert.ToByte(titles.getP4()),
                Convert.ToByte(titles.getP5()),
                Convert.ToByte(titles.getP6()),
                (byte)0, (byte)0
            });
            //3 байта - одетые перки
            WriteB(new byte[3]
            {
                Convert.ToByte(titles.getEquipedTitle1()),
                Convert.ToByte(titles.getEquipedTitle2()),
                Convert.ToByte(titles.getEquipedTitle3()),
            });
            WriteD(titles.getSlotCount());//сколько слотов для перок доступно(1-2-3)

            WriteD(0);
            WriteD(0x03);// Общий матч (0х03)
            WriteD(0x19);// Подрыв (0х19)
            WriteD(0);// Разрушение (0х23) / 37
            WriteD(0x01);// Выживание (0х01)
            WriteD(0);// Оборона (0х27)
            WriteD(0);// (0х01)
            WriteD(0);// (0х28)
            WriteD(0);// (1) хз
            WriteD(0);// (1) хз

            WriteD(0);// (0)
            WriteD(0);// (0)

            WriteD(0x36);// (0х36) боты?

            /*WriteC(36);// кол-во карт
            WriteC(1);// кол-во частей

            int Mask = 0;

            Mask |= 1 << (int)Maps.StageId.TD_PORTAKABA;
            Mask |= 1 << (int)Maps.StageId.TD_REDROCK;
            Mask |= 1 << (int)Maps.StageId.TD_LIBRARY;
            Mask |= 1 << (int)Maps.StageId.TD_MSTATION;
            Mask |= 1 << (int)Maps.StageId.TD_MIDNIGHTZONE;
            Mask |= 1 << (int)Maps.StageId.TD_UPTOWN;
            Mask |= 1 << (int)Maps.StageId.TD_BURNINGHALL;
            Mask |= 1 << (int)Maps.StageId.TD_DSQUAD;
            Mask |= 1 << (int)Maps.StageId.TD_CRACKDOWN;
            Mask |= 1 << (int)Maps.StageId.TD_EASTERNROAD;
            Mask |= 1 << (int)Maps.StageId.TD_BREAKDOWN;
            Mask |= 1 << (int)Maps.StageId.TD_TRAININGCAMP;
            Mask |= 1 << (int)Maps.StageId.TD_SENTRYBASE;
            Mask |= 1 << (int)Maps.StageId.TD_DESERTCAMP;
            Mask |= 1 << (int)Maps.StageId.TD_KICKPOINT;
            Mask |= 1 << (int)Maps.StageId.TD_FACEROCK;
            Mask |= 1 << (int)Maps.StageId.TD_SUPPLYBASE;
            Mask |= 1 << (int)Maps.StageId.TD_SANDSTORM;
            Mask |= 1 << (int)Maps.StageId.TD_SAFARI;
            Mask |= 1 << (int)Maps.StageId.TD_MACHUPICHU;
            Mask |= 1 << (int)Maps.StageId.TD_TWOTOWERS;
            Mask |= 1 << (int)Maps.StageId.TD_ANGKORRUINS;
            Mask |= 1 << (int)Maps.StageId.TD_GHOSTTOWN;
            Mask |= 1 << (int)Maps.StageId.TD_METRO;
            // подрыв
            Mask |= 1 << (int)Maps.StageId.BB_DOWNTOWN;
            Mask |= 1 << (int)Maps.StageId.BB_LUXVILLE;
            Mask |= 1 << (int)Maps.StageId.BB_OUTPOST;
            Mask |= 1 << (int)Maps.StageId.BB_BLOWCITY;
            Mask |= 1 << (int)Maps.StageId.BB_STORMTUBE;
            Mask |= 1 << (int)Maps.StageId.BB_SENTRYBASE;
            Mask |= 1 << (int)Maps.StageId.BB_HOSPITAL;
            Mask |= 1 << (int)Maps.StageId.BB_DOWNTOWN2;
            Mask |= 1 << (int)Maps.StageId.BB_SANDSTORM;
            Mask |= 1 << (int)Maps.StageId.BB_CARGOSHIP;
            Mask |= 1 << (int)Maps.StageId.BB_AIRPORT;
            Mask |= 1 << (int)Maps.StageId.BB_SAFEHOUSE;



            WriteD(Mask);

            WriteB(new byte[] {
                0x01, 0x00,
                0x00, 0x00,
                0x01, 0x00,
                0x01, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x01, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
                0x00, 0x00,
            });

            // иконки нью и прочее | по одной на карту
            WriteB(new byte[]{
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            });*/

            WriteC(60);
            WriteC(2);
            WriteB(new byte[]{
            (byte) 0xFE, (byte) 0xFF, (byte) 0xFE, (byte) 0xBF, (byte) 0xCF, 0x77, 0x07, 0x02,
        });
            //Карты, режимы
            WriteB(new byte[]{
                0x00, 0x00, (byte) 0x8D, 0x01, (byte) 0x88, 0x00, (byte) 0x89, 0x00, (byte) 0x8D, 0x00,
                (byte) 0x8D, 0x00, (byte) 0x8D, 0x00, (byte) 0x8D, 0x01, 0x09, 0x00, 0x01, 0x00, 0x00, 0x00,
                (byte) 0x8D, 0x00, (byte) 0x80, 0x00, (byte) 0x88, 0x00, (byte) 0x88, 0x00, (byte) 0x88, 0x00,
                0x00, 0x00, 0x00, 0x00, (byte) 0x8C, 0x00, (byte) 0x88, 0x00, (byte) 0x88, 0x00, (byte) 0x88,
                0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, (byte) 0x80, 0x00, (byte) 0x80, 0x00, 0x00, 0x00,
                (byte) 0x80, 0x00, (byte) 0x80, 0x00, (byte) 0x80, 0x00, (byte) 0x80, 0x00, (byte) 0x80, 0x00,
                (byte) 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        });
            WriteB(new byte[]{
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00,
                0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01,
        });

            WriteC(1);
            WriteB(new byte[]
            {
            (byte)0xEE, (byte)0x03, 0x03, 0x00, // 0xee - отвечают за обучающие и начальные миссии человека и дино и доп миссии;0xff- количество миссий;0x03- нужно проверять.
            });
            WriteD(0);//PC_CAFE
            WriteD(0);//PC_CAFE
            WriteC(1);//1-0
            WriteH(0);//Длина текста
            WriteD((player.getRank() == 53 || player.getRank() == 54) ? 1 : 0);//приходят пакеты 2686 и 2688 - ГМ
            WriteD(0);//Ледянец - 702001024
            WriteC(1);// Должна быть починка 
            WriteB(new byte[5]);// ХЗ что тут , еще не разобрал
        }

        private void LoadEquip()
        {
            WriteD(equip.getCharRed()); // Скин Мужчина стандартный красные
            WriteD(equip.getCharBlue()); // Скин Мужчина стандартный синие
            WriteD(equip.getCharHelmet()); // Шлем поидеи... надо тестить
            WriteD(equip.getCharBeret()); // Берет
            WriteD(0); // Хз что это. Влазиет пистолеты, ножи, снайпы, пулеметы
            WriteD(equip.getWeaponPrimary()); // Основное оружие
            WriteD(equip.getWeaponSecondary()); // Второстепенное оружие
            WriteD(equip.getWeaponMelee()); // Ближнего боя
            WriteD(equip.getWeaponThrownNormal()); // Гранаты (Гранаты для взрыва)
            WriteD(equip.getWeaponThrownSpecial()); // Гранаты (Шранаты специальные, смок, слеповуха) 
        }
        private void LoadItems()
        {
            WriteD(player.getInvetoryOnly(2).Count); // количество предметов в слоте "Солдат"
            WriteD(player.getInvetoryOnly(1).Count); // количество предметов в слоте "Оружие"
            WriteD(player.getInvetoryOnly(3).Count); // количество предметов в слоте "Купоны"
            WriteD(0); // количество предметов в новом слоте
            // Солдат
            for (int i = 0; i < player.getInvetoryOnly(2).Count; i++)
            {
                WriteQ(player.getInvetoryOnly(2)[i].ItemType == 3 ? 0 : player.getInvetoryOnly(2)[i].Id);
                WriteD(player.getInvetoryOnly(2)[i].ItemId);
                WriteC((byte)player.getInvetoryOnly(2)[i].Type);
                WriteD(player.getInvetoryOnly(2)[i].Count);
            }
            // Оружие
            for (int i = 0; i < player.getInvetoryOnly(1).Count; i++)
            {
                WriteQ(player.getInvetoryOnly(1)[i].ItemType == 3 ? 0 : player.getInvetoryOnly(1)[i].Id);
                WriteD(player.getInvetoryOnly(1)[i].ItemId);
                WriteC((byte)player.getInvetoryOnly(1)[i].Type);
                WriteD(player.getInvetoryOnly(1)[i].Count);
            }
            // Купоны
            for (int i = 0; i < player.getInvetoryOnly(3).Count; i++)
            {
                WriteQ(player.getInvetoryOnly(3)[i].ItemType == 3 ? 0 : player.getInvetoryOnly(3)[i].Id);
                WriteD(player.getInvetoryOnly(3)[i].ItemId);
                WriteC((byte)player.getInvetoryOnly(3)[i].Type);
                WriteD(player.getInvetoryOnly(3)[i].Count);
            }
        }

        public Tutorial getMissionById(int id)
        {
            foreach (Tutorial mission in TutorialParser.tutorial.Values)
            {
                if (mission.getId() == id)
                {
                    return mission;
                }
            }
            return null;
        }
    }
}
