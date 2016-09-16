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
using System.Xml;
using Core.Model;

namespace Core.Data.Parsers
{
    public class GameServersParser
    {
        public static SortedDictionary<int, GameServer> _servers = new SortedDictionary<int, GameServer>();
        public static void Load()
        {
            String path = "Data//Gameservers.xml";
            XmlDocument xmlDocument = new XmlDocument();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            if (fileStream.Length != 0L)
            {
                try
                {
                    xmlDocument.Load((Stream)fileStream);
                    for (XmlNode xmlNode1 = xmlDocument.FirstChild; xmlNode1 != null; xmlNode1 = xmlNode1.NextSibling)
                    {
                        if ("List".Equals(xmlNode1.Name))
                        {
                            for (XmlNode xmlNode2 = xmlNode1.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
                            {
                                if ("GameServer".Equals(xmlNode2.Name))
                                {
                                    XmlNamedNodeMap xmlNamedNodeMap = xmlNode2.Attributes;
                                    GameServer server = new GameServer
                                    {
                                        Name = xmlNamedNodeMap.GetNamedItem("Name").Value,
                                        Id = int.Parse(xmlNamedNodeMap.GetNamedItem("Id").Value),
                                        Type = int.Parse(xmlNamedNodeMap.GetNamedItem("Type").Value),
                                        MaxPlayers = int.Parse(xmlNamedNodeMap.GetNamedItem("MaxPlayers").Value),
                                        Ip = xmlNamedNodeMap.GetNamedItem("Ip").Value,
                                        Port = int.Parse(xmlNamedNodeMap.GetNamedItem("Port").Value),
                                    };
                                    _servers.Add(server.Id, server);
                                }
                            }
                        }
                    }
                    Logger.Info("[GameServers] Loaded {0} gameservers", _servers.Count);
                }
                catch (XmlException ex)
                {
                    Logger.Info("Error {0}" ,ex);
                }
                fileStream.Close();
            }
        }
    }
}
