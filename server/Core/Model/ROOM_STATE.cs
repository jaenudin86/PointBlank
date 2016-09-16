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
    public enum ROOM_STATE
    {
        ROOM_STATE_READY,
        ROOM_STATE_COUNTDOWN,
        ROOM_STATE_LOADING,
        ROOM_STATE_RENDEZVOUS,
        ROOM_STATE_PRE_BATTLE,
        ROOM_STATE_BATTLE,
        ROOM_STATE_BATTLE_END,
        ROOM_STATE_EMPTY,
    }
}
