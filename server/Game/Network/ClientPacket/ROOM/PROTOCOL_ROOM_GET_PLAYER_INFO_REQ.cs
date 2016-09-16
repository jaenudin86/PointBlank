using Game.Network.ServerPacket.ROOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ClientPacket.ROOM
{
    public class PROTOCOL_ROOM_GET_PLAYER_INFO_REQ : ReceivePacket
    {
        private int slot;

        public PROTOCOL_ROOM_GET_PLAYER_INFO_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {
            slot = ReadD();
        }

        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_ROOM_GET_PLAYER_INFO_ACK(getClient().getPlayer(), slot));
        }
    }
}
