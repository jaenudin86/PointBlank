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
    public class PlayerMedals
    {
        public ulong PlayerID;
        public int Ribbons;
        public int Badges;
        public int Medals;
        public int MasterMedals;

        public int getRibbons()
        {
            return Ribbons;
        }

        public void setRibbons(int Ribbons)
        {
            this.Ribbons = Ribbons;
        }

        public int getBadges()
        {
            return Badges;
        }

        public void setBadges(int Badges)
        {
            this.Badges = Badges;
        }

        public int getMedals()
        {
            return Medals;
        }

        public void setMedals(int Medals)
        {
            this.Medals = Medals;
        }

        public int getMaterMedals()
        {
            return MasterMedals;
        }

        public void setMasterMedals(int MasterMedals)
        {
            this.MasterMedals = MasterMedals;
        }
    }
}
