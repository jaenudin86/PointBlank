/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
namespace Core.Model
{
    public class PlayerConfig
    {
        public ulong PlayerID = 0;
        public int AudioEnable;
        public int Audio1;
        public int Audio2;
        public int Config;
        public int Mao;
        public int Visibility;
        public int Blood;
        public int MouseSensitivity;
        public int Vision;

        public int getAudioEnable()
        {
            return AudioEnable;
        }

        public int getAudio1()
        {
            return Audio1;
        }

        public int getAudio2()
        {
            return Audio2;
        }

        public int getConfig() 
        {
            return Config;
        }

        public int getMao() 
        {
            return Mao;
        }

        public int getVisibility()
        {
            return Visibility;
        }

        public int getBlood()
        {
            return Blood;
        }

        public int getMouseSensitivity()
        {
            return MouseSensitivity;
        }

        public int getVision()
        {
            return Vision;
        }
    }
}
