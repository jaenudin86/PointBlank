/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
namespace Core.Model.Missions
{
    public class Tutorial
    {
        public int Id;
        public int Mission1;
        public int Mission2;
        public int Mission3;
        public int Mission4;
        public int EXP;
        public int Points;
        public int Item;
        public int Type1;
        public int Type2;
        public int Type3;
        public int Type4;

        public int getId()
        {
            return Id;
        }

        public int getMission1()
        {
            return Mission1;
        }

        public int getMission1_Type()
        {
            return Type1;
        }

        public int getMission2()
        {
            return Mission2;
        }

        public int getMission2_Type()
        {
            return Type2;
        }

        public int getMission3()
        {
            return Mission3;
        }

        public int getMission3_Type()
        {
            return Type3;
        }

        public int getMission4()
        {
            return Mission4;
        }

        public int getMission4_Type()
        {
            return Type4;
        }

        public int getEXP()
        {
            return EXP;
        }

        public int getPoints()
        {
            return Points;
        }

        public int getItem()
        {
            return Item;
        }
    }
}
