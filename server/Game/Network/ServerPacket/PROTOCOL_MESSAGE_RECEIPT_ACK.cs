using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket
{
    public class PROTOCOL_MESSAGE_RECEIPT_ACK : SendPacket
    {
        private string author;
        private string message;

        public PROTOCOL_MESSAGE_RECEIPT_ACK(string author, string message)
        {
            this.author = author;
            this.message = message;
        }

        public override void WriteImpl()
        {
            WriteH(427);
            WriteD(0);
            WriteD(0);
            WriteD(2);
            WriteD(3);
            WriteC(4);
            WriteC(5);
            WriteC(6);
            WriteH((short) author.Length);
            WriteS(author, author.Length);
            WriteS(message, message.Length);
        }
    }
}
