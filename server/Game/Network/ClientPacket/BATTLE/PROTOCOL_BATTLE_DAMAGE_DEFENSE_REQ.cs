using Core.Model;
using Game.Network.ServerPacket.BATTLE;

namespace Game.Network.ClientPacket.BATTLE
{
    public class PROTOCOL_BATTLE_DAMAGE_DEFENSE_REQ : ReceivePacket
    {
        private int unk;
        private int unk2;
        private int unk3;
        private int unk4;

        public PROTOCOL_BATTLE_DAMAGE_DEFENSE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            unk = ReadC();
            unk2 = ReadC();
            unk3 = ReadC();
            unk4 = ReadC();
        }

        public override void RunImpl()
        {
            foreach (Player member in getClient().getPlayer().getRoom().getPlayers().Values)
            {
                member.getClient().SendPacket(new PROTOCOL_BATTLE_DAMAGE_DEFENSE_ACK(unk, unk2, unk3, unk4));
            }
        }
    }
}
