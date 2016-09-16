/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Database.Tables;
using Game.Network.ServerPacket;

namespace Core.Model
{
    public class Room
    {
        public static int ROOM_NAME_SIZE = 23;
        public static int ROOM_PASSWORD_SIZE = 4;

        public static int[] TIMES = new int[]{3, 5, 7, 5, 10, 15, 20, 25, 30};
        public static int[] KILLS = new int[]{60, 80, 100, 120, 140, 160};
        public static int[] ROUNDS = new int[]{0, 3, 5, 7, 9};
        public static int[] RED_TEAM = new int[]{0, 2, 4, 6, 8, 10, 12, 14};
	    public static int[] BLUE_TEAM = new int[]{1, 3, 5, 7, 9, 11, 13, 15};

        public static Dictionary<ulong, Player> players = new Dictionary<ulong, Player>();

        public SLOT[] ROOM_SLOT = new SLOT[16];

        private int id;
        private String name;
        private int mapId;
        private int type;
        private int stage4v4;
        private int allWeapons;
        private int randomMap;
        private int limit;
        private int seeConf;
        private int autobalans;
        private Player leader;
        private int figth;
        private int slots;
        private String password = "";
        private int special;

        private int killMask; // Маска, позволяющая понять об окончании боя.
        private int timeLost; // Сколько cекунд осталось.

        private int redKills;
        private int redDeaths;

        private int blueKills;
        private int blueDeaths;

        private int aiCount;
        private int aiLevel;

        private int bomb;

        private int redWinRounds;
        private int blueWinRounds;

        public int redTeamCount;
        public int blueTeamCount;

        public SLOT_STATE getSlotState(int slot)
        {
            return ROOM_SLOT[slot].getState();
        }

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public int getMapId()
        {
            return mapId;
        }

        public void setMapId(int mapId)
        {
            this.mapId = mapId;
        }

        public int getType()
        {
            return type;
        }

        public void setType(int type)
        {
            this.type = type;
        }

        public int getStage4v4()
        {
            return stage4v4;
        }

        public void setStage4v4(int stage4v4)
        {
            this.stage4v4 = stage4v4;
        }

        public int getAllWeapons()
        {
            return allWeapons;
        }

        public void setAllWeapons(int allWeapons)
        {
            this.allWeapons = allWeapons;
        }

        public int getRandomMap()
        {
            return randomMap;
        }

        public void setRandomMap(int randomMap)
        {
            this.randomMap = randomMap;
        }

        public int getLimit()
        {
            return limit;
        }

        public void setLimit(int limit)
        {
            this.limit = limit;
        }

        public int getSeeConf()
        {
            return seeConf;
        }

        public void setSeeConf(int seeConf)
        {
            this.seeConf = seeConf;
        }

        public int getAutobalans()
        {
            return autobalans;
        }

        public void setAutobalans(int autobalans)
        {
            this.autobalans = autobalans;
        }

        public Player getLeader()
        {
            if (leader == null)
                setNewLeader();

            return leader;
        }

        public void setLeader(Player leader)
        {
            this.leader = leader;
        }

        public int isFigth()
        {
            return figth;
        }

        public void setFigth(int figth)
        {
            this.figth = figth;
        }

        public Dictionary<ulong, Player> getPlayers()
        {
            return players;
        }

        public int getSlots()
        {
            return slots;
        }

        public void setNewLeader()
        {
            for (int i = 0; i < 16; i++)
            {
                if (ROOM_SLOT[i].getState() != SLOT_STATE.SLOT_STATE_CLOSE && ROOM_SLOT[i].getState() != SLOT_STATE.SLOT_STATE_EMPTY)
                {
                    setLeader(ROOM_SLOT[i].getPlayer());
                    break;
                }
            }
        }

        public void setSlots(int count)
        {
            if (count == 0)
            {
                count = 1;
            }
            this.slots = count;
            for (int i = 0; i < ROOM_SLOT.Length; i++)
                if (i >= count)
                    ROOM_SLOT[i].setState(SLOT_STATE.SLOT_STATE_CLOSE); // Отключаем ненужные слоты.
        }

        public String getPassword()
        {
            return password;
        }

        public void setPassword(String password)
        {
            this.password = password;
        }

        public int getSpecial()
        {
            return special;
        }

        public void setSpecial(int special)
        {
            this.special = special;
        }

        public void removePlayer(Player player)
        {
            if (getRoomSlotByPlayer(player) != null)
            {
                getRoomSlotByPlayer(player).setState(SLOT_STATE.SLOT_STATE_EMPTY);
                getRoomSlotByPlayer(player).setPlayer(null);
            }
            players.Remove(player.PlayerID);
        }

        public void addPlayer(Player player)
        {
            foreach (SLOT slot in ROOM_SLOT) 
            {
                if (slot.getState() == SLOT_STATE.SLOT_STATE_EMPTY)
                {
                    slot.setPlayer(player);
                    slot.setState(SLOT_STATE.SLOT_STATE_NORMAL);
                    players.Add(player.PlayerID, player);
                    break;
                }
            }
        }

        public void addPlayerToLeaderTeam(Player player)
        {
            SLOT leaderSlot = getRoomSlotByPlayer(leader);
            if (!addPlayerToTeam(player, Array.BinarySearch(RED_TEAM, leaderSlot.getId()) >= 0 ? RED_TEAM : BLUE_TEAM))
            {
                addPlayer(player);
            }
        }

        private bool addPlayerToTeam(Player player, int[] team)
        {
            foreach (int slotId in team)
            {
                SLOT slot = getRoomSlot(slotId);
                if (slot.getState() == SLOT_STATE.SLOT_STATE_EMPTY)
                {
                    slot.setPlayer(player);
                    slot.setState(SLOT_STATE.SLOT_STATE_NORMAL);
                    players.Add(player.PlayerID, player);
                    return true;
                }
            }
            return false;
        }

        public SLOT getRoomSlotByPlayer(Player player)
        {
            foreach (SLOT slot in ROOM_SLOT)
            {
                if (player.Equals(slot.getPlayer()))
                {
                    return slot;
                }
            }
            return null;
        }

        public SLOT getRoomSlot(int slotId)
        {
            foreach (SLOT slot in ROOM_SLOT)
            {
                if (slot.getId() == slotId)
                {
                    return slot;
                }
            }
            return null;
        }

        public SLOT[] getRoomSlots()
        {
            return ROOM_SLOT;
        }

        public SLOT changeTeam(Player player, int team)
        {
            SLOT slot = getRoomSlotByPlayer(player);
            foreach (int teamSlot in team == 0 ? RED_TEAM : BLUE_TEAM)
            {
                SLOT rslot = ROOM_SLOT[teamSlot];
                if (player.Equals(rslot.getPlayer()) || rslot.getState() == SLOT_STATE.SLOT_STATE_EMPTY)
                {
                    slot.setPlayer(null);
                    slot.setState(SLOT_STATE.SLOT_STATE_EMPTY);
                    rslot.setPlayer(player);
                    rslot.setState(SLOT_STATE.SLOT_STATE_NORMAL);
                    return rslot;
                }
            }
            return null;
        }

        /**
         * Возвращяет сколько секунд должен работать бой.
         *
         * @return
         */
        public int getKillTime()
        {
            return TIMES[killMask >> 4];
        }

        /**
         * Возвращает сколько нужно раундов, либо убийств до окончания боя.
         *
         * @return
         */
        public int getKillsByMask()
        {
            if (killMask >> 4 < 3)
                return ROUNDS[killMask-1 & 15]; // Если бой по раундам.
            else
                return KILLS[killMask & 15]; // Если бой по кол-ву убийств.
        }

        public int getKillMask()
        {
            return killMask;
        }

        public void setTimeLost(int time)
        {
            timeLost = time;
        }

        public int getTimeLost()
        {
            return timeLost;
        }

        /**
         * Устанавливает маску для определения, когда нужно закончить бой.
         *
         * @param variants
         */
        public void setKillMask(int variants)
        {
            this.killMask = variants;
        }

        public int getRedKills()
        {
            return redKills;
        }

        public void setRedKills(int kills)
        {
            redKills = kills;
        }

        public int getRedDeaths()
        {
            return redDeaths;
        }

        public void setRedDeaths(int deaths)
        {
            redDeaths = deaths;
        }

        public int getBlueKills()
        {
            return blueKills;
        }

        public void setBlueKills(int kills)
        {
            blueKills = kills;
        }

        public int getBlueDeaths()
        {
            return blueDeaths;
        }

        public void setBlueDeaths(int deaths)
        {
            blueDeaths = deaths;
        }

        public int getAiCount()
        {
            return aiCount;
        }

        public void setAiCount(int aiCount)
        {
            this.aiCount = aiCount;
        }

        public int getAiLevel()
        {
            return aiLevel;
        }

        public void setAiLevel(int aiLevel)
        {
            this.aiLevel = aiLevel;
        }

        public void RoomTask(int channel, Room room)
        {
            int countPlayerPrestart = 0;

            foreach(Player player in room.getPlayers().Values)
            {
                SLOT slot = room.getRoomSlotByPlayer(player);
                if ((int)slot.getState() > 8 && (int)slot.getState() < 12)
                {
                    countPlayerPrestart++;
                }
            }
            SLOT leader = room.getRoomSlotByPlayer(room.getLeader());
            if ((int)leader.getState() < 13)
            {
                if (countPlayerPrestart == 0)
                {
                    if ((int)leader.getState() < 13)
                    {
                        foreach (Player member in room.getPlayers().Values)
                        {
                            SLOT playerSlot = room.getRoomSlotByPlayer(member);
                            if (playerSlot.getState() == SLOT_STATE.SLOT_STATE_BATTLE_READY)
                            {
                                playerSlot.setState(SLOT_STATE.SLOT_STATE_BATTLE);
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK(room, member));
                                foreach (Player toSend in room.getPlayers().Values)
                                {
                                    if (!toSend.Equals(member))
                                    {
                                        SLOT toSendplayerSlot = room.getRoomSlotByPlayer(toSend);
                                        if ((int)toSendplayerSlot.getState() > 11)
                                        {
                                            member.getClient().SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK(room, toSend));
                                        }
                                    }
                                }
                                member.getClient().SendPacket(new opcode_3865_ACK());
                                member.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_START(room));
                            }
                        }
                    }
                }
            }
            else
            {
                foreach(Player player in room.getPlayers().Values)
                {
                    SLOT playerSlot = room.getRoomSlotByPlayer(player);
                    if ((int)playerSlot.getState() == 12)
                    {
                        player.getClient().SendPacket(new opcode_3890_ACK(room));
                        playerSlot.setState(SLOT_STATE.SLOT_STATE_BATTLE);
                        player.getClient().SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK(room, player));
                        foreach(Player player1 in room.getPlayers().Values)
                        {
                            SLOT why = room.ROOM_SLOT[player1.getSlot()];
                            if ((int)why.getState() == 13 && !player.Equals(player1))
                            {
                                player1.getClient().SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK(room, player));
                                player.getClient().SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK(room, player1));
                            }
                        }
                        player.getClient().SendPacket(new opcode_3865_ACK());
                        player.getClient().SendPacket(new PROTOCOL_BATTLE_ROUND_START(room));
                    }

                }
            }

            foreach (Player member in room.getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(room));
            }

            Logger.Info("{0}",leader.getState());
        }

        public int getBombState()
        {
            return bomb;
        }

        public void setBombState(int bomb)
        {
            this.bomb = bomb;
        }

        public int getRedWinRounds()
        {
            return redWinRounds;
        }

        public void setRedWinRounds(int redWinRounds)
        {
            this.redWinRounds = redWinRounds;
        }

        public int getBlueWinRounds()
        {
            return blueWinRounds;
        }

        public void setBlueWinRounds(int blueWinRounds)
        {
            this.blueWinRounds = blueWinRounds;
        }


    }
}
