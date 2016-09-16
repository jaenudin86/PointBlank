/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Database.Tables;

namespace Core.Model
{
    public class Inventory
    {
        public static List<Item> getAllItemsOnType(int type, ulong ownerid)
        {
            List<Item> items = new List<Item>();
            foreach (Item im in ItemsTable.items[ownerid].ToArray())
            {
                if (im.Slot >= 0 && im.Slot < 5 && type == 1) { items.Add(im); }
                if (im.Slot > 4 && im.Slot < 10 && type == 2) { items.Add(im); }
                if (im.Slot > 9 && im.Slot < 16 && type == 3) { items.Add(im); }
            }
            return items;
        }

        public static Item getEquip(int slot, ulong ownerid)
        {
            Item item = new Item();
            foreach (Item im in ItemsTable.items[ownerid].ToArray())
            {
                if (im.Slot == slot) { item = im; }
            }
            return item;
        }

        public static Item getItemById(long id, ulong ownerid)
        {
            Item obj1 = new Item();
            foreach (Item obj2 in ItemsTable.items[ownerid].ToArray())
            {
                if ((long)obj2.Id == id)
                    obj1 = obj2;
            }
            return obj1;
        }
    }
}
