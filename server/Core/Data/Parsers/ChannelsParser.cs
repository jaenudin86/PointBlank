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
    public class ChannelsParser
    {
        public static List<Channel> _servers = new List<Channel>();
        public static void Load()
        {
            String path = "Data//Channels.xml";
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
                                if ("Channel".Equals(xmlNode2.Name))
                                {
                                    XmlNamedNodeMap xmlNamedNodeMap = xmlNode2.Attributes;
                                    Channel server = new Channel();
                                    /*{
                                        Id = int.Parse(xmlNamedNodeMap.GetNamedItem("Id").Value),
                                        Type = int.Parse(xmlNamedNodeMap.GetNamedItem("Type").Value),
                                        Announce = xmlNamedNodeMap.GetNamedItem("Announce").Value
                                    };*/
                                    server.setId(int.Parse(xmlNamedNodeMap.GetNamedItem("Id").Value));
                                    server.setType(int.Parse(xmlNamedNodeMap.GetNamedItem("Type").Value));
                                    server.setAnnounce(xmlNamedNodeMap.GetNamedItem("Announce").Value);

                                    _servers.Add(server);
                                }
                            }
                        }
                    }
                    Logger.Info("[Channels] Loaded {0} channels", _servers.Count);
                }
                catch (XmlException ex)
                {
                    Logger.Info("Error {0}", ex);
                }
                fileStream.Close();
            }
        }
    }
}
