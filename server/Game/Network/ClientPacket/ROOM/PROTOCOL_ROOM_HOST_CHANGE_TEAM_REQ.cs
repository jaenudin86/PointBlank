using Core.Model;
using Game.Network.ServerPacket;

namespace Game.Network.ClientPacket
{
    public class PROTOCOL_ROOM_HOST_CHANGE_TEAM_REQ : ReceivePacket
    {
        private int hostChanget = 1;

        public override void ReadImpl()
        {
            ReadH();
        }

        public override void RunImpl()
        {
            Player player = getClient().getPlayer();
            Room room = getClient().getPlayer().getRoom();
            foreach (Player player2 in player.getRoom().getPlayers().Values)
            {
                int oldSlot = player.getSlot();
                player2.setSlot(player.getSlot());
                int slot = player.getSlot();
                //this.hostChanget = room.slot(account2, slot, oldSlot, this.host_changed);
                foreach (Player player3 in player.getRoom().getPlayers().Values)
                {

                }
            }
        }
    }
}
