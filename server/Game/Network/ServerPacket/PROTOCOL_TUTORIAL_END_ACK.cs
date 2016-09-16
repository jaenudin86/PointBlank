/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using Core.Model;

namespace Game.Network.ServerPacket
{
    public class PROTOCOL_TUTORIAL_END_ACK : SendPacket
    {
        Player player;
        public override void WriteImpl()
        {
            WriteH(3395);
            WriteC(10);
            WriteC(11);
            WriteD(200);
            player.setRoom(null);
            
        }
    }
}
