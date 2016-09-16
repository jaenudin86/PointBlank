using Core.Database.Tables;
using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket.ROOM
{
    public class PROTOCOL_ROOM_GET_PLAYER_INFO_ACK : SendPacket
    {
        private Player player;
        private int slot;
        PlayerStats stats;
        PlayerEquip equip;

        public PROTOCOL_ROOM_GET_PLAYER_INFO_ACK(Player player, int slot)
        {
            this.player = player;
            this.slot = slot;
        }

        public override void WriteImpl()
        {
            WriteH((short)3842);

            if(player.getRoom().getSlotState(slot) == SLOT_STATE.SLOT_STATE_EMPTY)
            {
                WriteD(0);
            }
            else
            {
                SLOT slot = player.getRoom().getRoomSlot(this.slot);

                WriteD(this.slot);
                WriteS(slot.getPlayer().getName(), 33);
                WriteD(slot.getPlayer().getExp());
                WriteD(slot.getPlayer().getRank());
                WriteD(slot.getPlayer().getRank());
                WriteD(slot.getPlayer().getGp());
                WriteD(slot.getPlayer().getMoney());
                if (slot.getPlayer().getClanID() != 0)
                {
                    WriteD(1);
                    WriteD(1);
                }
                else
                    WriteB(new byte[8]);
                WriteD(0);
                WriteD(0);
                WriteH((short) slot.getPlayer().getPCCafe());
                WriteC(0);// color
                WriteS(slot.getPlayer().getClan().getName() , 17);
                WriteH(slot.getPlayer().getClan().getRank());
                WriteC((byte)slot.getPlayer().getClan().getLogo1());
                WriteC((byte)slot.getPlayer().getClan().getLogo2());
                WriteC((byte)slot.getPlayer().getClan().getLogo3());
                WriteC((byte)slot.getPlayer().getClan().getLogo4());
                WriteH(slot.getPlayer().getClan().getColor());
                WriteD(0);
                WriteD(0);
                WriteD(0);

                stats = PlayersStatsTable.statistics[slot.getPlayer().PlayerID];

                WriteD(stats.getFights());//всего боев 
                WriteD(stats.getWins());//всего побед
                WriteD(stats.getLosts());//всего поражений
                WriteD(0);
                WriteD(stats.getKills());//кол-во убийств
                WriteD(stats.getHeadshots());//кол-во хедшотов
                WriteD(stats.getDeaths());//кол-во смертей
                WriteD(0);
                WriteD(stats.getKills());//опять килы о.о
                WriteD(stats.getEscapes());//всего ливнул
                WriteD(stats.getSeasonFights());//всего боев за сезон
                WriteD(stats.getSeasonWins());//всего побед за сезон
                WriteD(stats.getSeasonLosts());//всего поражений за сезон
                WriteD(0);
                WriteD(stats.getSeasonKills());//киллы сезон по идее
                WriteD(stats.getSeasonHeadshots());//хеды сезон по идее
                WriteD(stats.getSeasonDeaths());//смерти сезон по идее
                WriteD(0);
                WriteD(stats.getSeasonKills());//опять килы,хз зачем
                WriteD(stats.getSeasonEscapes());//сколько ливнул за сезон

                equip = PlayerEquipTable.players_equip[slot.getPlayer().PlayerID];

                WriteD(equip.getCharRed()); // Скин Мужчина стандартный красные
                WriteD(equip.getCharBlue()); // Скин Мужчина стандартный синие
                WriteD(equip.getCharHelmet()); // Шлем поидеи... надо тестить
                WriteD(equip.getCharBeret()); // Берет
                WriteD(0); // Хз что это. Влазиет пистолеты, ножи, снайпы, пулеметы
                WriteD(equip.getWeaponPrimary()); // Основное оружие
                WriteD(equip.getWeaponSecondary()); // Второстепенное оружие
                WriteD(equip.getWeaponMelee()); // Ближнего боя
                WriteD(equip.getWeaponThrownNormal()); // Гранаты (Гранаты для взрыва)
                WriteD(equip.getWeaponThrownSpecial()); // Гранаты (Шранаты специальные, смок, слеповуха) 
            }
        }
    }
}
