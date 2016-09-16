using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Database.Tables;
using Core;
using Core.Model;

namespace PointBlank.Manager
{
    class AccountsManager : SingletonBase<AccountsManager>
    {
        public Player getPlayer(ulong AccountID)
        {
            return PlayersTable.players[AccountID];
        }
    }
}
