using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network.ClientPacket;
using Core;

namespace Game.Network
{
    class PacketHandler : SingletonBase<PacketHandler>
    {
        public static Dictionary<int, ReceivePacket> packets = new Dictionary<int, ReceivePacket>();

        public PacketHandler()
        {
            packets.Add(0xA13, new PROTOCOL_BASE_USER_ENTER_REQ());
            packets.Add(0xA0B, new PROTOCOL_BASE_ENTER_CHANNELSELECT_REQ());
            packets.Add(0xA0D, new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_REQ());
            packets.Add(0xD10, new opcode_3860_REQ());
            packets.Add(0xF16, new PROTOCOL_BASE_ENTER_PROFILE_REQ());
            //ЛОББИ
            packets.Add(0xD35, new PROTOCOL_LOBBY_JOIN_ROOM_REQ());
            packets.Add(0xC07, new PROTOCOL_LOBBY_ENTER_REQ());
            packets.Add(0xC01, new PROTOCOL_LOBBY_GET_ROOMLIST_REQ());
            packets.Add(0xC11, new PROTOCOL_LOBBY_CREATE_ROOM_REQ());
            packets.Add(0xB55, new opcode_2901_REQ());
            //ИНВЕНТАРЬ
            packets.Add(0xE01, new PROTOCOL_INVENTORY_ENTER_REQ());
            packets.Add(0xE05, new PROTOCOL_INVENTORY_LEAVE_REQ());
            packets.Add(0x216, new PROTOCOL_INVENTORY_USE_ITEM_REQ());
            packets.Add(0x21E, new PROTOCOL_INVENTORY_DELETE_ITEM_REQ());
            //КОМНАТА
            packets.Add(0xE0B, new PROTOCOL_ROOM_CLOSE_SLOT_REQ());
            packets.Add(0xF05, new PROTOCOL_ROOM_CHANGE_TEAM_REQ());
            //БОИ
            packets.Add(0xD2C, new PROTOCOL_BATTLE_TIMER_SYNC_REQ());
            packets.Add(0xD1C, new PROTOCOL_BATTLE_BOMB_TAB_REQ());
            packets.Add(0xD1E, new PROTOCOL_BATTLE_BOMB_UNTAB_REQ());
            packets.Add(0xD09, new PROTOCOL_BATTLE_RESPAWN_REQ());
            packets.Add(0xD32, new PROTOCOL_BATTLE_BOT_RESPAWN_REQ());
            packets.Add(0xD1A, new PROTOCOL_BATTLE_FRAG_INFO_REQ());
            packets.Add(0xD03, new PROTOCOL_BATTLE_READYBATTLE_REQ());
            packets.Add(0xD14, new PROTOCOL_BATTLE_PRESTARTBATTLE_REQ());
            packets.Add(0xD05, new PROTOCOL_BATTLE_STARTBATTLE_REQ());
            packets.Add(0xD30, new PROTOCOL_BATTLE_BOT_CHANGELEVEL_REQ());
            //МАГАЗИН
            packets.Add(0xB05, new PROTOCOL_SHOP_LIST_REQ());
            packets.Add(0xB03, new PROTOCOL_SHOP_ENTER_REQ());
            packets.Add(0x212, new PROTOCOL_SHOP_BUY_ITEM_REQ());
            packets.Add(0xB01, new PROTOCOL_SHOP_LEAVE_REQ());
            //КЛАНЫ
            packets.Add(0x5A1, new PROTOCOL_CLAN_ENTER_REQ());
            packets.Add(0x5A3, new PROTOCOL_CLAN_LEAVE_REQ());
            packets.Add(0x5A5, new PROTOCOL_CLAN_LIST_REQ());
            packets.Add(0x5AB, new PROTOCOL_CLAN_LIST_REQ());
            packets.Add(0x588, new PROTOCOL_CLAN_REQUESITES_FOR_CREATE_REQ());
            packets.Add(0x5A7, new PROTOCOL_CLAN_CHECK_NAME_REQ());
            packets.Add(0x51E, new PROTOCOL_CLAN_CREATE_REQ());
            packets.Add(0x518, new PROTOCOL_CLAN_INFO_REQ());

            //ЛИЧНЫЕ СООБЩЕНИЯ
            
            Logger.Info("[PacketHandler] Loaded {0} packets", packets.Count);
        }
    }
}
