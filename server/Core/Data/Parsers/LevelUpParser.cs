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
    public class LevelUpParser
    {
        public static List<Levels> _levels = new List<Levels>();
        public static void Load()
        {
            String path = "Data//LevelUp.xml";
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
                                if ("Level".Equals(xmlNode2.Name))
                                {
                                    XmlNamedNodeMap xmlNamedNodeMap = xmlNode2.Attributes;
                                    Levels levels = new Levels();
                                    levels.setRank(int.Parse(xmlNamedNodeMap.GetNamedItem("Rank").Value));
                                    levels.setOnNextLevel(int.Parse(xmlNamedNodeMap.GetNamedItem("onNextLevel").Value));
                                    levels.setOnGPUp(int.Parse(xmlNamedNodeMap.GetNamedItem("onGPUp").Value));
                                    levels.setItem(int.Parse(xmlNamedNodeMap.GetNamedItem("onItemUp").Value));
                                    levels.setOnAllExp(int.Parse(xmlNamedNodeMap.GetNamedItem("onAllExp").Value));

                                    _levels.Add(levels);
                                }
                            }
                        }
                    }
                    Logger.Info("[Ranks] Loaded {0} ranks", _levels.Count);
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
