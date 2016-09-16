using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket.MESSAGES
{
    public class PROTOCOL_MESSENGER_CHECK_ACK : SendPacket
    {
        public override void WriteImpl()
        {
            string message = "Hello, this is tested message!";
            WriteH((short)291);
            WriteD(0);
            WriteS("Admin", 33);
            WriteH((short) message.Length);
            WriteS(message, message.Length);
            
        }
    }
}
