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
     public enum SLOT_STATE
    {
        SLOT_STATE_EMPTY,
        SLOT_STATE_CLOSE,
        SLOT_STATE_SHOP,
        SLOT_STATE_INFO,
        SLOT_STATE_CLAN,
        SLOT_STATE_INVENTORY,
        SLOT_STATE_OUTPOST,
        SLOT_STATE_NORMAL,
        SLOT_STATE_READY,
        SLOT_STATE_LOAD,
        SLOT_STATE_RENDEZVOUS,
        SLOT_STATE_PRESTART,
        SLOT_STATE_BATTLE_READY,
        SLOT_STATE_BATTLE,
    } 
}
