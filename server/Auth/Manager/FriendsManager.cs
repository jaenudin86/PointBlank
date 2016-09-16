/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using Core;
using Core.Database.Tables;
using Core.Model;
using System.Collections.Generic;

namespace PointBlank.Manager
{
    public class FriendsManager
    {
        public static List<Friend> getFriendsByPlayerId(ulong PlayerID)
        {
            return FriendsTable.friends[PlayerID];
        }
    }
}