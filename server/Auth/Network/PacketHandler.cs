using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using PointBlank.Network.ClientPacket;

namespace PointBlank.Network
{
    class PacketHandler : SingletonBase<PacketHandler>
    {
        public static Dictionary<int, ReceivePacket> packets = new Dictionary<int, ReceivePacket>();

        public PacketHandler()
        {
            packets.Add(0xA01, new PROTOCOL_BASE_LOGIN_WEBKEY_RUSSIA_REQ());
            packets.Add(0xA05, new PROTOCOL_BASE_GET_MYINFO_REQ());
            packets.Add(0xA11, new PROTOCOL_BASE_USER_LEAVE_REQ());
            packets.Add(0xA6A, new opcode_2667_REQ());
            packets.Add(0xA76, new opcode_2678_REQ());
            //packets.Add(0xA07, new PROTOCOL_AUTH_FRIEND_INFO_REQ());
        }
    }
}
