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
    class PROTOCOL_BATTLE_FRAG_INFO_ACK : SendPacket
    {
        private Room room;
        private FragInfos fragInfos;

        public PROTOCOL_BATTLE_FRAG_INFO_ACK(Room room, FragInfos fragInfos)
        {
            this.room = room;
            this.fragInfos = fragInfos;
        }

        public override void WriteImpl()
        {
            WriteH(0xD1B);
            WriteC((byte)fragInfos.getVicTimIdx()); // WTF?
            WriteC((byte)fragInfos.getKillsCount()); // Всего померло.
            WriteC((byte)fragInfos.getKiller()); // Слот убитого.
            WriteD(fragInfos.getKillWeaapon()); // Орудие убийства.
            WriteB(fragInfos.getUnkBytes()); // Хз что там.

            SLOT killer = room.getRoomSlot(fragInfos.getKiller());

            for (int i = 1; i <= fragInfos.getKillsCount(); i++)
            {
                Frag frag = fragInfos.getFrag(i);
                WriteC((byte)frag.getUnkC1());
                WriteC((byte)frag.getDeathMask()); //
                switch (killer.getKillMessage())
                {
                    case 0:
                        WriteH(0);//обычный килл
                        Logger.Warn("Type: 0");
                        break;
                    case 1:
                        WriteH(1);//в голову по идее
                        Logger.Warn("Type: 1");
                        break;
                    case 2:
                        WriteH(2);//головорез?
                        Logger.Warn("Type: 2");
                        break;
                    case 3:
                        WriteH(4);//эт мастер
                        Logger.Warn("Type: 4");
                        break;
                    case 4:
                        WriteH(8);
                        Logger.Warn("Type: 8");
                        break;
                    case 5:
                        WriteH(16);
                        Logger.Warn("Type: 9");
                        break;
                    case 6:
                        WriteH(32);
                        Logger.Warn("Type: 32");
                        break;
                    case 7:
                        WriteH(64);
                        Logger.Warn("Type: 64");
                        break;
                    case 8:
                        WriteH(128);
                        Logger.Warn("Type: 128");
                        break;
                    default:
                        WriteH(0);
                        Logger.Warn("Type: 0");
                        break;
                }
                WriteB(frag.getUnk13bytes());
            }
            WriteD(room.getRedKills());

            WriteD(room.getBlueKills());

            foreach (SLOT member in room.getRoomSlots()) 
            {
                WriteH((short)member.getAllKills());
                WriteH((short)member.getAllDeath());
            }
            if (killer.getOneTimeKills() == 1)
            {
                WriteC(0); // кол-вол игроков?
            }
            else if (killer.getOneTimeKills() == 2)
            {
                WriteC(1); // кол-вол игроков?
            }
            else if (killer.getOneTimeKills() == 3)
            {
                WriteC(2); // кол-вол игроков?
            }
            else if (killer.getOneTimeKills() > 3)
            {
                WriteC(3); // кол-вол игроков?
            }
            WriteH((short)killer.getBotScore());
        }
    }
}
