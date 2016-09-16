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
    public class Item
    {
        public int ItemId;
        public int Id;
        public int Slot;
        public ulong OwnerId;
        //public int Equip;
        public int Type;
        public int Count;
        public int ItemType;
    }
}
