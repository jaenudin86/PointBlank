/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Core.Config
{
    class ConfigLoader
    {
        private FileInfo ConfigFile;
        public SortedList<string, string> _topics;

        public ConfigLoader(string Path)
        {
            ConfigFile = new FileInfo(Path);
            _topics = new SortedList<string, string>();
            Load();
        }

        public void Load()
        {
            StreamReader stream = new StreamReader(ConfigFile.FullName);
            while (!stream.EndOfStream)
            {
                string line = stream.ReadLine();
                if (line.Length == 0) continue;
                if (line.StartsWith(";")) continue;
                _topics.Add(line.Split('=')[0], line.Split('=')[1]);
            }
            Logger.Info("[Config] Loaded {0} parametrs", _topics.Count);
        }

        public string getValue(string value, string defaultprop)
        {
            string ret;
            try 
            { 
                ret = _topics[value]; 
            }
            catch 
            { 
                return defaultprop; 
            }
            return ret == null ? defaultprop : ret;
        }
    }
}
