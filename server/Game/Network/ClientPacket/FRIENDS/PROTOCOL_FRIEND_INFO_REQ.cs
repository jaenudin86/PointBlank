using Game.Network.ServerPacket.FRIENDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ClientPacket.FRIENDS
{
    public class PROTOCOL_FRIEND_INFO_REQ : ReceivePacket
    {
        public PROTOCOL_FRIEND_INFO_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }

        public override void ReadImpl()
        {

        }

        public override void RunImpl()
        {
            getClient().SendPacket(new PROTOCOL_FRIEND_INFO_ACK());
        }
    }
}
