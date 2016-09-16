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
    public class Levels
    {
        public int ItemId = 0;
        public int onAllExp = 0;
        public int onGPUp = 0;
        public int onNextLevel = 0;
        public int Rank = 0;
        public string Name;

        public int getItem()
        {
            return ItemId;
        }

        public void setItem(int ItemId)
        {
            this.ItemId = ItemId;
        }

        public int getOnAllExp()
        {
            return onAllExp;
        }

        public void setOnAllExp(int onAllExp)
        {
            this.onAllExp = onAllExp;
        }

        public int getOnGPUp()
        {
            return onGPUp;
        }

        public void setOnGPUp(int onGPUp)
        {
            this.onGPUp = onGPUp;
        }

        public int getOnNextLevel()
        {
            return onNextLevel;
        }

        public void setOnNextLevel(int onNextLevel)
        {
            this.onNextLevel = onNextLevel;
        }

        public int getRank()
        {
            return Rank;
        }

        public void setRank(int Rank)
        {
            this.Rank = Rank;
        }

        public string getName()
        {
            return Name;
        }

        public void setName(string Name)
        {
            this.Name = Name;
        }
    }
}
