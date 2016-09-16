/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using Core.Model;
using System.Net;
using Core.Config;

namespace Game.Network.ServerPacket
{
    class PROTOCOL_BATTLE_PRESTARTBATTLE_ACK : SendPacket
    {
        private Room room;
        private Player player;

        public PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(Room room, Player player)
        {
            this.player = player;
            this.room = room;
        }

        public override void WriteImpl()
        {

            WriteH(0xD15);
            WriteD((short)room.isFigth()); // 
            WriteD(room.getRoomSlotByPlayer(player).getId());
            /*
             *  общий матч - 1
                уничтожение - 4
                боты - 1
                подрыв - 2
                танк ебаный - 5
                ебучие дизозавры - 7
                тренировка - 10
             */
            switch (player.getRoom().getType())
            {
                case 1://общий матч
                    WriteC(1);
                    break;
                case 2://подрыв
                    WriteC(2);
                    break;
                case 3://unk
                    WriteC(1);
                    break;
                case 4://уничтожение
                    WriteC(2);
                    break;
                case 6:///unk
                    WriteC(1);
                    break;
                case 7://динозавры
                    WriteC(2);
                    break;
                case 10://тренировка
                    WriteC(1);
                    break;
            }
            WriteB(room.getLeader().getAddress());
            WriteH(29890);
            WriteB(room.getLeader().getAddress());
            WriteH(29890);
            WriteC(0);
            WriteB(player.getAddress());
            WriteH(29890);
            WriteB(player.getAddress());
            WriteH(29890);
            WriteC(0);
            WriteB(IPAddress.Parse(ConfigModel.BATTLE_SERVER).GetAddressBytes());
            //int port = 40000;
            WriteB(new byte[] { 0x40, 0x9c });
            WriteB(new byte[]
				{
						(byte) 0x91,
						0x00,
						0x00,
						0x00,
						(byte) 0x47,
						0x06,
						0x00,
						0x00
				});

            WriteC(0);

            WriteB(new byte[]
				{
						0x0a,
						0x22,
						0x00,
						0x01,
						0x10,
						0x03,
						0x1e,
						0x05,
						0x06,
						0x07,
						0x04,
						0x09,
						0x16,
						0x0b,
						0x1b,
						0x08,
						0x0e,
						0x0f,
						0x02,
						0x11,
						0x12,
						0x21,
						0x14,
						0x15,
						0x13,
						0x17,
						0x18,
						0x19,
						0x1a,
						0x0c,
						0x1c,
						0x1d,
						0x0d,
						0x1f,
						0x20
				});
        }
    }
}
