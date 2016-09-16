/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public class SLOT
    {
        private int id;
        private int allKills, oneTimeKills;
        private int lastKillMessage, killMessage;
        private int allDeath;
        private int botScore;
        private Player player;
        private int Headshots;

        private int allExp;
        private int allGP;

        public int lastKillState;
        public bool repeatLastState;

        private SLOT_STATE state = SLOT_STATE.SLOT_STATE_EMPTY;

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public Player getPlayer()
        {
            return player;
        }

        public void setPlayer(Player player)
        {
            this.player = player;
        }

        public SLOT_STATE getState()
        {
            return state;
        }

        public void setState(SLOT_STATE state)
        {
            this.state = state;
        }

        public void setHeadshots(int Headshots)
        {
            this.Headshots = Headshots;
        }

        public int getHeadshots()
        {
            return Headshots;
        }

        public void setAllKills(int kills)
        {
            allKills = kills;
        }

        public int getAllKills()
        {
            return allKills;
        }

        public void setOneTimeKills(int kills)
        {
            oneTimeKills = kills;
        }

        public int getOneTimeKills()
        {
            return oneTimeKills;
        }

        public void setLastKillMessage(int message)
        {
            lastKillMessage = message;
        }

        public int getLastKillMessage()
        {
            return lastKillMessage;
        }

        public int getKillMessage()
        {
            return killMessage;
        }

        public void setKillMessage(int message)
        {
            killMessage = message;
        }

        public void setAllDeahts(int death)
        {
            allDeath = death;
        }

        public int getAllDeath()
        {
            return allDeath;
        }

        public void setBotScore(int score)
        {
            botScore = score;
        }

        public int getBotScore()
        {
            return botScore;
        }

        public int getAllExp()
        {
            return allExp;
        }

        public int getAllGp()
        {
            return allGP;
        }

        public void setAllExp(int allExp)
        {
            this.allExp = allExp;
        }

        public void setAllGP(int allGP)
        {
            this.allGP = allGP;
        }
    }
}
