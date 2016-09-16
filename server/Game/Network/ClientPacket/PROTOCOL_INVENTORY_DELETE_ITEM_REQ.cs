using Core.Database.Tables;
using Core.Model;
using Game.Network;
using Game.Network.ServerPacket;

namespace Game.Network.ClientPacket
{
    internal class PROTOCOL_INVENTORY_DELETE_ITEM_REQ : ReceivePacket
    {
        private long id;

        public override void ReadImpl()
        {
            id = ReadQ();
        }

        public override void RunImpl()
        {
            Item itemById = getClient().getPlayer().getItemById(id);
            ItemsTable.items[getClient().getPlayer().PlayerID].Remove(itemById);
            getClient().SendPacket((SendPacket)new PROTOCOL_INVENTORY_DELETE_ITEM_ACK(id));
        }
    }
}
