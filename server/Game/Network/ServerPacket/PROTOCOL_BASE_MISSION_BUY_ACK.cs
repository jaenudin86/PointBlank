/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Model;

namespace Game.Network.ServerPacket
{
    public class PROTOCOL_BASE_MISSION_BUY_ACK : SendPacket
    {
        private int MissionID;
        private Player player;

        public PROTOCOL_BASE_MISSION_BUY_ACK(int missionId, Player p)
        {
             this.MissionID = missionId;
             this.player = p;
        }

        public override void WriteImpl()
        {
            WriteH((short)2606);
            WriteD(0);
            WriteD(this.player.getGp());
            WriteB(new byte[5]);
            WriteC((byte)9);
            WriteC((byte)9);
            WriteC((byte)2);
            WriteB(new byte[60]);
            WriteB(new byte[6]
            {
                byte.MaxValue,
                byte.MaxValue,
                byte.MaxValue,
                byte.MaxValue,
                (byte) 0,
                (byte) 240
            });
            WriteB(new byte[14]);
            WriteC((byte) MissionID);
            byte[] numArray = new byte[3];
            numArray[2] = (byte)13;
            WriteB(numArray);
        }
    }
}
