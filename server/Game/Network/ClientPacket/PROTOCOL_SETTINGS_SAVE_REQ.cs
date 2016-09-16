/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Network.ServerPacket;
using Core.Database.Tables;


namespace Game.Network.ClientPacket
{
    class PROTOCOL_SETTINGS_SAVE_REQ : ReceivePacket
    {
        private int AudioEnable;
        private int Blood;
        private int Config;
        private int Mao;
        private int Visibility;
        private int music;
        private int MouseSensitivity;
        private int sound;
        private int Vision;

        public PROTOCOL_SETTINGS_SAVE_REQ(GameNetwork Client, byte[] data)
        {
            makeme(Client, data);
        }
        public override void ReadImpl()
        {
            ReadB(4);
            Blood = ReadC();
            int num1 = ReadC();
            Visibility = ReadC();
            Mao = ReadC();
            Config = ReadC();
            ReadB(3);
            AudioEnable = ReadC();
            ReadB(5);
            music = ReadC();
            sound = ReadC();
            Vision = ReadC();
            int num2 = ReadC();
            MouseSensitivity = ReadC();
        }
        public override void RunImpl()
        {
            Logger.Info("Sound: " + sound + " Music: " + music);
            PlayersConfigTable.LoadTable();
            PlayersConfigTable.SaveConfigs(getClient().getPlayer().PlayerID, Config, Blood, Visibility, Mao, music, sound, AudioEnable, MouseSensitivity, Vision);
        }
    }
}
