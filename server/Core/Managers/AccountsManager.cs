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
using Core;
using Core.Model;

namespace Core.Managers
{
    class AccountsManager : SingletonBase<AccountsManager>
    {
        public Player getPlayer(ulong AccountID)
        {
            return PlayersTable.players[AccountID];
        }
    }
}