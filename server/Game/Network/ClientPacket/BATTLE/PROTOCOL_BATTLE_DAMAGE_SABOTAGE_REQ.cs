using Core.Model;
using Game.Network.ServerPacket.BATTLE;

namespace Game.Network.ClientPacket.BATTLE
{
    public class PROTOCOL_BATTLE_DAMAGE_SABOTAGE_REQ : ReceivePacket
    {
        private int u;
        private int u2;
        private int u3;
        private int u4;
        private int u5;
        private int u6;
        private int u7;
        private int u8;

        public PROTOCOL_BATTLE_DAMAGE_SABOTAGE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            u = ReadC();
            u2 = ReadC();
            u3 = ReadC();
            u4 = ReadC();
            u5 = ReadC();
            u6 = ReadC();
            u7 = ReadC();
            u8 = ReadC();
        }

        public override void RunImpl()
        {
            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_BATTLE_DAMAGE_SABOTAGE_ACK(u, u2, u3, u4, u5, u6, u7, u8));
            }
        }
    }
}
