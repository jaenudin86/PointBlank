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
    public class Good
    {
        public int good_id, item_id ,priceCredits, pricePoints, stockType, quantity, type, slot;

        public int ItemType;

        public int getItemType()
        {
            return ItemType;
        }

        public int getGoodId()
        {
            return good_id;
        }

        public void setGoodId(int good_id)
        {
            this.good_id = good_id;
        }

        public int getItemId()
        {
            return item_id;
        }

        public void setItemId(int item_id)
        {
            this.item_id = item_id;
        }

        public int getQuantity()
        {
            return quantity;
        }

        public void setQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public int getPriceCredits()
        {
            return priceCredits;
        }

        public void setPriceCredits(int priceCredits)
        {
            this.priceCredits = priceCredits;
        }

        public int getPricePoints()
        {
            return pricePoints;
        }

        public void setPricePoints(int pricePoints)
        {
            this.pricePoints = pricePoints;
        }

        public int getStockType()
        {
            return stockType;
        }

        public void setStockType(int stockType)
        {
            this.stockType = stockType;
        }

        public int getLifeType()
        {
            return type;
        }

        public void setLifeType(int type)
        {
            this.type = type;
        }

        public int getSlot()
        {
            return slot;
        }

        public void setSlot(int slot)
        {
            this.slot = slot;
        }

    }
}
