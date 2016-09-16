/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace Core.Model
{
    public class FragInfos
    {
        private int victimIdx; // Не известно
        private int killer; // Кто убил
        private int killsCount; // Сколько человек померло
        private int weapon; // Оружие которым убило

        private byte[] bytes13; // Не разобранные байты.

        public static Dictionary<int, Frag> frags = new Dictionary<int, Frag>();

        /**
         * Устанавливает слот убившего.
         *
         * @param id - номер cлота
         */
        public void setKiller(int id)
        {
            killer = id;
        }

        /**
         * Возвращает слот убившего.
         *
         * @return int номер слота
         */
        public int getKiller()
        {
            return killer;
        }

        /**
         * Добавляет обьект типа {@link Frag}
         *
         * @param i    - Позиция
         * @param frag - Смерть
         */
        public void addFrag(int i, Frag frag)
        {
            frags.Remove(i);
            frags.Add(i, frag);
        }

        /**
         * Возвращает одну смерть из указанной позиции.
         *
         * @param i - Позиция
         * @return - Смерть
         */
        public Frag getFrag(int i)
        {
            return frags[i];
        }

        /**
         * Устанавливает каким оружием было совершено событие убийства.
         *
         * @param id
         */
        public void setKillWeapon(int id)
        {
            weapon = id;
        }

        /**
         * Возвращает каким оружием было совершено событие убийства.
         *
         * @return int
         */
        public int getKillWeaapon()
        {
            return weapon;
        }

        /**
         * Возвращает картинку оружия которым попали в голову.
         *
         * @return
         */
        public int getWeaponHeadNum()
        {
            return weapon / 100000;
        }

        /**
         * Устанавливает сколько всего было Смертей в данном событии убийства.
         *
         * @param count
         */
        public void setKillsCount(int count)
        {
            killsCount = count;
        }

        /**
         * Возвращает сколько всего было Смертей в данном событии убийства.
         *
         * @return
         */
        public int getKillsCount()
        {
            return killsCount;
        }

        /**
         * Пока точно не известно.
         *
         * @param id
         */
        public void setVicTimIdx(int id)
        { // ИД команды убийци? красн, син?
            victimIdx = id;
        }

        /**
         * Пока точно не известно.
         *
         * @return
         */
        public int getVicTimIdx()
        {
            return victimIdx;
        }

        /**
         * Неразобранные байты.
         *
         * @param bytes
         */
        public void setUnkBytes(byte[] bytes)
        {
            bytes13 = bytes;
        }

        /**
         * Неразобранные байты.
         *
         * @return
         */
        public byte[] getUnkBytes()
        {
            return bytes13;
        }
    }
}
