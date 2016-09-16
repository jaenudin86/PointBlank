/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
namespace Core.Model
{
    public class PlayerStats
    {
        public ulong PlayerID = 0;
        public int Fights;
        public int Wins;
        public int Losts;
        public int Kills;
        public int Headshots;
        public int Deaths;
        public int Escapes;
        public int SeasonFights;
        public int SeasonWins;
        public int SeasonLosts;
        public int SeasonKills;
        public int SeasonHeadshots;
        public int SeasonDeaths;
        public int SeasonEscapes;

        public int getFights()
        {
            return Fights;
        }

        public void setFights(int Fights)
        {
            this.Fights = Fights;
        }

        public int getWins()
        {
            return Wins;
        }

        public void setWins(int Wins)
        {
            this.Wins = Wins;
        }

        public int getLosts()
        {
            return Losts;
        }

        public void setLosts(int Losts)
        {
            this.Losts = Losts;
        }

        public int getKills()
        {
            return Kills;
        }

        public void setKills(int Kills)
        {
            this.Kills = Kills;
        }

        public int getHeadshots()
        {
            return Headshots;
        }

        public void setHeadshots(int Headshots)
        {
            this.Headshots = Headshots;
        }

        public int getDeaths()
        {
            return Deaths;
        }

        public void setDeaths(int Deaths)
        {
            this.Deaths = Deaths;
        }

        public int getEscapes()
        {
            return Escapes;
        }

        public void setEscapes(int Escapes)
        {
            this.Escapes = Escapes;
        }

        /* Сезон инфо */
        public int getSeasonFights()
        {
            return SeasonFights;
        }

        public void setSeasonFights(int SeasonFights)
        {
            this.SeasonFights = SeasonFights;
        }

        public int getSeasonWins()
        {
            return SeasonWins;
        }

        public void setSeasonWins(int SeasonWins)
        {
            this.SeasonWins = SeasonWins;
        }

        public int getSeasonLosts()
        {
            return SeasonLosts;
        }

        public void setSeasonLosts(int SeasonLosts)
        {
            this.SeasonLosts = SeasonLosts;
        }

        public int getSeasonKills()
        {
            return SeasonKills;
        }

        public void setSeasonKills(int SeasonKills)
        {
            this.SeasonKills = SeasonKills;
        }

        public int getSeasonHeadshots()
        {
            return SeasonHeadshots;
        }

        public void setSeasonHeadshots(int SeasonHeadshots)
        {
            this.SeasonHeadshots = SeasonHeadshots;
        }

        public int getSeasonDeaths()
        {
            return SeasonDeaths;
        }

        public void setSeasonDeaths(int SeasonDeaths)
        {
            this.SeasonDeaths = SeasonDeaths;
        }

        public int getSeasonEscapes()
        {
            return SeasonEscapes;
        }

        public void setSeasonEscapes(int SeasonEscapes)
        {
            this.SeasonEscapes = SeasonEscapes;
        }
    }
}
