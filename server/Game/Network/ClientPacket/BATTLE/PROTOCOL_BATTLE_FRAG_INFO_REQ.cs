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
using Game.Managers;
using Game.Network.ServerPacket;
using System.Threading;
using Core.Database.Tables;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BATTLE_FRAG_INFO_REQ : ReceivePacket
    {
        private FragInfos fragInfos = new FragInfos();
        private int TeamWin = -1;

        public PROTOCOL_BATTLE_FRAG_INFO_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            fragInfos.setVicTimIdx(ReadC()); // WTF? killsCount - 1, если не убиваеш себя... если и себя то 0 будет если пострадал 1 бот и сам игрок.
            fragInfos.setKillsCount(ReadC()); // Всего померло.
            fragInfos.setKiller(ReadC()); // Слот убившего.
            fragInfos.setKillWeapon(ReadD()); // Орудие убийства.
            fragInfos.setUnkBytes(ReadB(13)); // Хз что там

            for (int i = 1; i <= fragInfos.getKillsCount(); i++)
            {
                Frag frag = new Frag();
                frag.setUnkC1(ReadC()); //
                frag.setDeathMask(ReadC()); //
                frag.setUnkC3(ReadC()); //
                frag.setUnkC4(ReadC()); //
                frag.setUnk13bytes(ReadB(13));

                fragInfos.addFrag(i, frag);
            }
            Logger.Info("Frags {0}",fragInfos.getKillsCount());
        }

        public override void RunImpl()
        {
            Room room = getClient().getPlayer().getRoom();
            Player player = getClient().getPlayer();
            if (room != null && player != null)
            {
                SLOT killer = room.getRoomSlot(fragInfos.getKiller());
                for (int i = 1; i <= fragInfos.getKillsCount(); i++)
                {
                    Frag frag = fragInfos.getFrag(i);
                    if (frag != null)
                    {
                        bool suicide = frag.getDeatSlot() == fragInfos.getKiller();
                        SLOT death = room.getRoomSlot(frag.getDeatSlot());

                        if (!suicide)
                        {

                            killer.setAllKills(killer.getAllKills() + 1);

                            /* Подсчет опыта и очков */
                            killer.setAllExp(killer.getAllExp() + 9);//сохраняем опыт
                            killer.setAllGP(killer.getAllGp() + 5);//сохраняем очки

                            killer.setOneTimeKills(killer.getOneTimeKills() + 1);
                            int weaponHeadNum = fragInfos.getWeaponHeadNum();
                            killer.setKillMessage(0);
                            if (fragInfos.getKillsCount() > 1)
                            {
                                if ((weaponHeadNum == 8030) || (weaponHeadNum == 9030))
                                {
                                    killer.setKillMessage(2);
                                }
                                else
                                {
                                    killer.setKillMessage(1);
                                }
                            }
                            else
                            {
                                int killMessage = 0;
                                if (frag.getDeathMask() >> 4 == 3)
                                { //TODO:: понять что это и всунуть в Frag!
                                    killMessage = 4;
                                }
                                else if ((frag.getDeathMask() >> 4 == 1) && (frag.getDeathMask() >> 2 == 1) && (weaponHeadNum == 7020))
                                {
                                    killMessage = 6;
                                }

                                if (killMessage > 0)
                                {
                                    int lastMessage = killer.lastKillState >> 12;

                                    if (killMessage == 4)
                                    {
                                        if (lastMessage != 4)
                                        {
                                            killer.repeatLastState = false;
                                            //killer.setOneTimeKills(0);
                                        }

                                        killer.setOneTimeKills(killer.getOneTimeKills() + 1);
                                        killer.lastKillState = killMessage << 12 | killer.getOneTimeKills();

                                        int countedKill = killer.lastKillState & 0xF;

                                        if (killer.repeatLastState)
                                        {
                                            if (countedKill > 1)
                                                killer.setKillMessage(5);
                                            else
                                                killer.setKillMessage(4);
                                        }
                                        else
                                        {
                                            killer.setKillMessage(4);
                                            killer.repeatLastState = true;
                                        }
                                    }
                                    else if (killMessage == 6)
                                    {
                                        if (lastMessage != 6)
                                        {
                                            killer.repeatLastState = false;
                                           // killer.setOneTimeKills(0);
                                        }

                                        killer.setOneTimeKills(killer.getOneTimeKills() + 1);
                                        killer.lastKillState = killMessage << 12 | killer.getOneTimeKills();

                                        int countedKill = killer.lastKillState & 0xF;

                                        if (killer.repeatLastState)
                                        {
                                            if (countedKill > 1)
                                                killer.setKillMessage(6);
                                        }
                                        else
                                            killer.repeatLastState = true;
                                    }
                                }
                                else
                                {
                                    killer.lastKillState = 0;
                                    killer.repeatLastState = false;
                                }
                            }
                                //
                        }
                        //Добавляем общий счет команде
                        if (frag.getDeatSlot() % 2 == 0)
                        {    // Если помер красный
                            room.setBlueKills(room.getBlueKills() + 1);
                            room.setRedDeaths(room.getRedDeaths() + 1);
                        }
                        else
                        {
                            room.setRedKills(room.getRedKills() + 1);
                            room.setBlueDeaths(room.getBlueDeaths() + 1);
                        }
                        death.setOneTimeKills(0);
                        death.setKillMessage(0);
                        death.setLastKillMessage(0);
                        death.lastKillState = 0;

                        death.setAllDeahts(death.getAllDeath() + 1);
                        //Добавляем очки в бою с ботами
                        if (room.getSpecial() == 6)
                        {
                            SLOT slot = room.getRoomSlot(fragInfos.getKiller());
                            int AILevel = room.getRoomSlotByPlayer(room.getLeader()).getId() % 2 == 0 ? room.getAiLevel() + room.getBlueDeaths() / 20 : room.getAiLevel() + room.getRedDeaths() / 20;
                            int AIScore = 10 + room.getRoomSlot(fragInfos.getKiller()).getOneTimeKills() * AILevel;
                            slot.setBotScore(slot.getBotScore() + AIScore);
                        }

                        /* Миссии */
                        if (room.getSpecial() != 6)
                        {
                            SLOT killer2 = room.getRoomSlot(fragInfos.getKiller());

                            if(getClient().getPlayer() == killer2.getPlayer())
                            {
                                for (int count = 0; count < fragInfos.getKillsCount(); count++)
                                {
                                    getClient().SendPacket(new PROTOCOL_BASE_MISSION_COMPLETE_ACK(242, 1));//убийство
                                }

                                if (killer2.getKillMessage() == 8)
                                {
                                    getClient().SendPacket(new PROTOCOL_BASE_MISSION_COMPLETE_ACK(244, 1));//хедшот
                                }

                                if (killer2.getKillMessage() == 9)
                                {
                                    getClient().SendPacket(new PROTOCOL_BASE_MISSION_COMPLETE_ACK(244, 1));//тож хедшот
                                }
                            }

                            if (death == room.getRoomSlotByPlayer(getClient().getPlayer()))
                            {
                                getClient().SendPacket(new PROTOCOL_BASE_MISSION_COMPLETE_ACK(241, 1));//смерть
                            }
                        }

                        if (room.getType() == 2)
                        {
                            if (room.getBlueKills() == room.redTeamCount)
                            {
                                TeamWin = 1;
                            }
                            else if (room.getRedKills() == room.blueTeamCount)
                            {
                                TeamWin = 0;
                            }
                        }
                        /* Уничтожение */
                        if (room.getType() == 4)
                        {
                            if (room.getBlueKills()  == room.redTeamCount)
                            {
                                TeamWin = 1;
                            }
                            else if (room.getRedKills() == room.blueTeamCount)
                            {
                                TeamWin = 0;
                            }
                        }
                    }
                }
                foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                {
                    if (SLOT_STATE.SLOT_STATE_BATTLE == room.getRoomSlotByPlayer(member).getState())
                    {
                        member.getClient().SendPacket(new PROTOCOL_BATTLE_FRAG_INFO_ACK(room, fragInfos));
                    }
                }
                if (TeamWin >= 0)
                {
                    //TeamWin = -1;
                    if (!(TeamWin == 1 && room.getBombState() == 1))
                    {
                        room.setRedKills(0);
                        room.setBlueKills(0);
                        if (TeamWin == 1)
                        {
                            room.setBlueWinRounds(room.getBlueWinRounds() + 1);
                        }
                        else
                        {
                            room.setRedWinRounds(room.getRedWinRounds() + 1);
                        }
                        if ((TeamWin == 1 ? room.getBlueWinRounds() : room.getRedWinRounds()) == room.getKillsByMask())
                        {
                            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                            {
                                SLOT slot = room.getRoomSlotByPlayer(member);
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_END_ACK(member, room));
                                slot.setKillMessage(0);
                                slot.setLastKillMessage(0);
                                slot.setOneTimeKills(0);
                                slot.setAllKills(0);
                                slot.setAllDeahts(0);
                            }
                            room.setRedKills(0);
                            room.setRedDeaths(0);
                            room.setBlueKills(0);
                            room.setBlueDeaths(0);
                            room.setFigth(0);
                            room.setBlueWinRounds(0);
                            room.setRedWinRounds(0);
                            room.setBombState(0);
                        }
                        else
                        {
                            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                            {
                                SLOT slot = room.getRoomSlotByPlayer(member);
                                slot.setKillMessage(0);
                                slot.setLastKillMessage(0);
                                slot.setOneTimeKills(0);
                                slot.lastKillState = 0;
                                room.setRedKills(0);
                                room.setRedDeaths(0);
                                room.setBlueKills(0);
                                room.setBlueDeaths(0);
                                room.setBombState(0);
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_END_ACK(TeamWin, 4, getClient().getPlayer().getRoom()));
                            }
                            Thread.Sleep(8000);
                            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
                            {
                                member.getClient().SendPacket(new opcode_3865_ACK());
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_START(member.getRoom()));
                            }
                        }
                        //TeamWin = -1;
                    }
                    TeamWin = -1;
                }
            }
        }
    }
}
