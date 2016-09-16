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
    public class PlayerEquip
    {
        public ulong PlayerID;
        public int WeaponPrimary;
        public int WeaponSecondary;
        public int WeaponMelee;
        public int WeaponThrownNormal;
        public int WeaponThrownSpecial;
        public int CharRed;
        public int CharBlue;
        public int CharHelmet;
        public int CharDino;
        public int CharBeret;

        public int getWeaponPrimary()
        {
            return WeaponPrimary;
        }

        public int getWeaponSecondary()
        {
            return WeaponSecondary;
        }

        public int getWeaponMelee()
        {
            return WeaponMelee;
        }

        public int getWeaponThrownNormal()
        {
            return WeaponThrownNormal;
        }

        public int getWeaponThrownSpecial()
        {
            return WeaponThrownSpecial;
        }

        public int getCharRed()
        {
            return CharRed;
        }

        public int getCharBlue()
        {
            return CharBlue;
        }

        public int getCharHelmet()
        {
            return CharHelmet;
        }

        public int getCharDino()
        {
            return CharDino;
        }

        public int getCharBeret()
        {
            return CharBeret;
        }
    }
}
