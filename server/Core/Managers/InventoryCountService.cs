using Core.Model;
using Core.Database.Tables;
using System.Collections.Generic;

namespace Core.Managers
{
    public class InventoryCountService
    {
        public static Dictionary<int, List<BattleCount>> players;
        private static int itemSet = 0;

        /* Создание массива */
        public static void Create()
        {
            players = new Dictionary<int, List<BattleCount>>();
        }

        /* Добавление предмета в массив */
        public static void addItem(int PlayerID, int ItemID)
        {
            foreach (var player in players)
            {
                BattleCount item = new BattleCount()
                {
                    PlayerID = PlayerID,
                    ItemID = ItemID
                };

                if (!players.ContainsKey(PlayerID))
                {
                    players.Add(PlayerID, new List<BattleCount>());
                }

                foreach (var itemArray in players[PlayerID].ToArray())
                {
                    if (itemArray.ItemID == ItemID)
                    {
                        itemSet = 1;
                    }
                }

                if(itemSet == 0)
                {
                    players[PlayerID].Add(item);
                }
            }
        }

        /* Обновление количества предметов в инвентаре */
        public static void Final()
        {
            foreach (var player in players)
            {
                foreach(var item in players[player.Key].ToArray())
                {
                    Item itemFinal = Inventory.getItemById(item.PlayerID, (ulong)item.ItemID);

                    itemFinal.Count = itemFinal.Count - 1;
                    
                    if(itemFinal.Count > 0)
                    {
                        ItemsTable.UpdateQuantity((ulong)itemFinal.Id, itemFinal.Count);
                    }
                    else
                    {
                        ItemsTable.DelItem(itemFinal.Id);
                    }
                }
            }
        }

        /* Удаление игрока */
        public static void deletePlayer(int PlayerID)
        {
            foreach (var player in players)
            {
                foreach (var item in players[player.Key].ToArray())
                {
                    players.Remove(item.PlayerID);
                }
            }
        }

        /* Удаление массива */
        public static void Clear()
        {
            players.Clear();
        }
    }
}
