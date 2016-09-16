using Core.Model;
using Game.Network.ServerPacket;
using System.Threading;
using Core.Enums;

namespace Game.Network.ClientPacket
{
    class PROTOCOL_BATTLE_STARTBATTLE_REQ : ReceivePacket
    {
        public PROTOCOL_BATTLE_STARTBATTLE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            
        }
        public override void RunImpl()
        {
            Player player = getClient().getPlayer();
            Room room = player.getRoom();
            SLOT slot = room.getRoomSlotByPlayer(getClient().getPlayer());
            getClient().SendPacket(new opcode_3890_ACK(room));
            getClient().SendPacket(new PROTOCOL_BATTLE_ROOMINFO_ACK(room));

          //  if (room.getLeader().Equals(getClient().getPlayer()))
           // {
               getClient().getPlayer().getRoom().getRoomSlotByPlayer(getClient().getPlayer()).setState(SLOT_STATE.SLOT_STATE_BATTLE_READY);
                room.RoomTask(getClient().getPlayer().ChannelId, room);
          //  }
            //room.RoomTask(getClient().getPlayer().ChannelId, room);
             Logger.Info("START  {0}", slot.getState());
            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_ROOM_INFO_ACK(room));
                Thread.Sleep(2000);
                getClient().SendPacket(new PROTOCOL_MESSAGE_ALL_PLAYERS_ACK());
            }
            //TODO: Доделать
            if (room.getSpecial() != 6)
            {
                //getClient().SendPacket(new PROTOCOL_BASE_MISSION_COMPLETE_ACK((int)Mission.MISSION_ENTER, 1));//миссия выполнена - вход в бой
                getClient().SendPacket(new PROTOCOL_BASE_MISSION_COMPLETE_ACK(243, 1));//миссия выполнена - смерть
            }
        }
    }
}
