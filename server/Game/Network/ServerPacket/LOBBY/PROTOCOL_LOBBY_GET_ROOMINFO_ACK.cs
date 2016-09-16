using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Network.ServerPacket.LOBBY
{
    public class PROTOCOL_LOBBY_GET_ROOMINFO_ACK : SendPacket
    {
        Room room;

        public PROTOCOL_LOBBY_GET_ROOMINFO_ACK(Room room)
        {
            this.room = room;
        }

        public override void WriteImpl()
        {
            WriteH(3088);
            WriteS(room.getLeader().PlayerName, 33);
            WriteC((byte) room.getKillTime());
            WriteC((byte) (room.getRedWinRounds() + room.getBlueWinRounds()));
            WriteH((short) room.getKillTime());
            WriteC((byte) room.getLimit());
            WriteC((byte) room.getSeeConf());
            WriteH((short) room.getAutobalans());
        }
    }
}
