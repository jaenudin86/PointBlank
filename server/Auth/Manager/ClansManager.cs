using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Core.Model;
using Core.Database.Tables;

namespace PointBlank.Manager
{
    class ClansManager : SingletonBase<ClansManager>
    {
        public Clan getClanById(ulong id)
        {
            foreach (Clan im in ClansTable.clans.Values)
            {
                if (im.Id == id)
                    return im;
            }
            return null;
        }

        public Dictionary<ulong, Clan> getClans()
        {
            return ClansTable.clans;
        }
    }
}
