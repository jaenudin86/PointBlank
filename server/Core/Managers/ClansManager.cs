using System.Collections.Generic;
using Core.Model;
using Core.Database.Tables;

namespace Core.Managers
{
    public class ClansManager : SingletonBase<ClansManager>
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

        public Dictionary<ulong, Player> getPlayersForClan(int ClanID)
        {
            Dictionary<ulong, Player> players;
            players = new Dictionary<ulong, Player>();

            foreach (var player in PlayersTable.players.Values)
            {
                if(player.getClanID() == ClanID)
                {
                    players.Add(player.PlayerID, player);
                }
            }

            return players;
        }

        public Clan getClanForName(string name)
        {
            foreach (Clan clan in ClansTable.clans.Values)
            {
                if (clan.Name == name)
                    return clan;
            }
            return null;
        }

        public Dictionary<ulong, Clan> getClans()
        {
            return ClansTable.clans;
        }
    }
}
