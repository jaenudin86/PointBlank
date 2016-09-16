using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Model;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_LOBBY_CHATING_ACK : SendPacket
    {
        Player player;
        string message;
        public PROTOCOL_LOBBY_CHATING_ACK(Player player, string message)
        {
            this.player = player;
            this.message = message;
        }
        public override void WriteImpl()
        {
            WriteH(0xC15);
            WriteD(1);
            WriteC(50);
            WriteS("OZNetwork");
            WriteC(0);
            WriteH(4);
            WriteS("Test");
        }
    }
}
