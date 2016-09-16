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
    public class Frag
    {
        private int unk_c_1;
        private int deathMask;
        private int unk_c_3;
        private int unk_c_4;
        private byte[] bytes13;
        /**
         * Пока что не известно.
         *
         * @param unk
         */
        public void setUnkC1(int unk)
        {
            unk_c_1 = unk;
        }


        public int getUnkC1()
        {
            return unk_c_1;
        }

        /**
         * Пока что не известно.
         *
         * @param unk
         */
        public void setUnkC3(int unk)
        {
            unk_c_3 = unk;
        }

        /**
         * Пока что не известно.
         *
         * @return
         */
        public int getUnkC3()
        {
            return unk_c_3;
        }

        /**
         * Пока что не известно.
         *
         * @param unk
         */
        public void setUnkC4(int unk)
        {
            unk_c_4 = unk;
        }

        /**
         * Пока что не известно.
         *
         * @return
         */
        public int getUnkC4()
        {
            return unk_c_4;
        }

        /**
         * Пока что не известно.
         *
         * @param unk
         */
        public void setUnk13bytes(byte[] unk)
        {
            bytes13 = unk;
        }

        /**
         * Пока что не известно.
         *
         * @return
         */
        public byte[] getUnk13bytes()
        {
            return bytes13;
        }

        /**
         * Возвращяет слот умершего.
         *
         * @return
         */
        public int getDeatSlot()
        {
            return deathMask & 15;
        }

        /**
         * Устанавливает маску для вычисления информации о Смерти
         *
         * @param mask
         */
        public void setDeathMask(int mask)
        {
            deathMask = mask;
        }

        public int getDeathMask()
        {
            return deathMask;
        }

    }
}
