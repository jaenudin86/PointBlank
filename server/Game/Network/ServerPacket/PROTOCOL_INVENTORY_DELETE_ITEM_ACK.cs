using Game.Network;

namespace Game.Network.ServerPacket
{
    internal class PROTOCOL_INVENTORY_DELETE_ITEM_ACK : SendPacket
    {
        private long id;

        public PROTOCOL_INVENTORY_DELETE_ITEM_ACK(long id)
        {
            id = id;
        }

        public override void WriteImpl()
        {
            this.WriteH(543);
            this.WriteD(1);
            this.WriteQ(id);
        }
    }
}