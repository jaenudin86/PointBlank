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
    public class QuestCardSet
    {
        public ulong PlayerID;
        public int MissionID;
        public int CardID;
        public int Card1_1,Card1_2,Card1_3,Card1_4,Card2_1,Card2_2,Card2_3,Card2_4,Card3_1,Card3_2,Card3_3,Card3_4,Card4_1,Card4_2,Card4_3,Card4_4,Card5_1,Card5_2,Card5_3,Card5_4,Card6_1,Card6_2,Card6_3,Card6_4,Card7_1,Card7_2,Card7_3,Card7_4,Card8_1,Card8_2,Card8_3,Card8_4,Card9_1,Card9_2,Card9_3,Card9_4,Card10_1,Card10_2,Card10_3,Card10_4;
        public int LastRewardEXP;
        public int LastRewardCredits;

        public int getCard1_1() { return Card1_1; }
        public int getCard1_2() { return Card1_2; }
        public int getCard1_3() { return Card1_3; }
        public int getCard1_4() { return Card1_4; }
        public int getCard2_1() { return Card2_1; }
        public int getCard2_2() { return Card2_2; }
        public int getCard2_3() { return Card2_3; }
        public int getCard2_4() { return Card2_4; }
        public int getCard3_1() { return Card3_1; }
        public int getCard3_2() { return Card3_2; }
        public int getCard3_3() { return Card3_3; }
        public int getCard3_4() { return Card3_4; }
        public int getCard4_1() { return Card4_1; }
        public int getCard4_2() { return Card4_2; }
        public int getCard4_3() { return Card4_3; }
        public int getCard4_4() { return Card4_4; }
        public int getCard5_1() { return Card5_1; }
        public int getCard5_2() { return Card5_2; }
        public int getCard5_3() { return Card5_3; }
        public int getCard5_4() { return Card5_4; }
        public int getCard6_1() { return Card6_1; }
        public int getCard6_2() { return Card6_2; }
        public int getCard6_3() { return Card6_3; }
        public int getCard6_4() { return Card6_4; }
        public int getCard7_1() { return Card7_1; }
        public int getCard7_2() { return Card7_2; }
        public int getCard7_3() { return Card7_3; }
        public int getCard7_4() { return Card7_4; }
        public int getCard8_1() { return Card8_1; }
        public int getCard8_2() { return Card8_2; }
        public int getCard8_3() { return Card8_3; }
        public int getCard8_4() { return Card8_4; }
        public int getCard9_1() { return Card9_1; }
        public int getCard9_2() { return Card9_2; }
        public int getCard9_3() { return Card9_3; }
        public int getCard9_4() { return Card9_4; }
        public int getCard10_1() { return Card10_1; }
        public int getCard10_2() { return Card10_2; }
        public int getCard10_3() { return Card10_3; }
        public int getCard10_4() { return Card10_4; }

        public int getMissionID()
        {
            return MissionID;
        }

        public int getCardID()
        {
            return CardID;
        }

        

        public int getLastRewardEXP()
        {
            return LastRewardEXP;
        }

        public int getLastRewardCredits()
        {
            return LastRewardCredits;
        }
    }
}
