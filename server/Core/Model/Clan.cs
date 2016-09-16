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
    public class Clan
    {
        public static int CLAN_NAME_SIZE = 17;
        public ulong Id;
        public String Name;
        public short Rank;
        public int Exp;
        public ulong OwnerId;
        public int DateCreated; //временно
        public short Color;
        public short Logo1;
        public short Logo2;
        public short Logo3;
        public short Logo4;
        public int MaxPlayersCount;
        public int PlayersCount;
        public String Info;
        public String Notice;



        public ulong getId()
        {
            return Id;
        }

        public void setId(ulong Id)
        {
            this.Id = Id;
        }

        public String getName()
        {
            return Name;
        }

        public void setName(String Name)
        {
            this.Name = Name;
        }

        public short getRank()
        {
            return Rank;
        }

        public void setRank(short Rank)
        {
            this.Rank = Rank;
        }

        public ulong getOwnerId()
        {
            return OwnerId;
        }

        public void setOwnerId(ulong OwnerId)
        {
            this.OwnerId = OwnerId;
        }

        public void setDateCreated(int date)
        {
            this.DateCreated = date;
        }

        public int getDateCreated()
        {
            return DateCreated;
        }

        public short getColor()
        {
            return Color;
        }

        public void setColor(short Color)
        {
            this.Color = Color;
        }

        public short getLogo1()
        {
            return Logo1;
        }

        public void setLogo1(short Logo1)
        {
            this.Logo1 = Logo1;
        }

        public short getLogo2()
        {
            return Logo2;
        }

        public void setLogo2(short Logo2)
        {
            this.Logo2 = Logo2;
        }

        public short getLogo3()
        {
            return Logo3;
        }

        public void setLogo3(short Logo3)
        {
            this.Logo3 = Logo3;
        }

        public short getLogo4()
        {
            return Logo4;
        }

        public void setLogo4(short Logo4)
        {
            this.Logo4 = Logo4;
        }

        public String toString()
        {
            return "Clan{" +
                    "id=" + Id +
                    ", name='" + Name + '\'' +
                    ", rank=" + Rank +
                    ", ownerId=" + OwnerId +
                    ", dateCreated=" + DateCreated +
                    ", color=" + Color +
                    ", logo1=" + Logo1 +
                    ", logo2=" + Logo2 +
                    ", logo3=" + Logo3 +
                    ", logo4=" + Logo4 +
                    '}';
        }

        public Object[] toObject()
        {

            Object[] args = new Object[8];
            args[0] = this.getName();
            args[1] = this.getRank();
            args[2] = this.getOwnerId();
            args[3] = this.getColor();
            args[4] = this.getLogo1();
            args[5] = this.getLogo2();
            args[6] = this.getLogo3();
            args[7] = this.getLogo4();
            return args;
        }
    }
}
