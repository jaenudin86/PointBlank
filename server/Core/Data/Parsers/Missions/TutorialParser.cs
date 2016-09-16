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
using Core.Model.Missions;

namespace Core.Data.Parsers.Missions
{
    public class TutorialParser
    {
        public static Dictionary<int, Tutorial> tutorial = new Dictionary<int, Tutorial>();

        public static void Load()
        {
            String path = "Data//Missions//Tutorial.xml";
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
                                if ("Card".Equals(xmlNode2.Name))
                                {
                                    XmlNamedNodeMap xmlNamedNodeMap = xmlNode2.Attributes;
                                    Tutorial mission = new Tutorial
                                    {
                                        Id = int.Parse(xmlNamedNodeMap.GetNamedItem("Id").Value),
                                        Mission1 = int.Parse(xmlNamedNodeMap.GetNamedItem("Mission1").Value),
                                        Type1 = int.Parse(xmlNamedNodeMap.GetNamedItem("Type1").Value),
                                        Mission2 = int.Parse(xmlNamedNodeMap.GetNamedItem("Mission2").Value),
                                        Type2 = int.Parse(xmlNamedNodeMap.GetNamedItem("Type2").Value),
                                        Mission3 = int.Parse(xmlNamedNodeMap.GetNamedItem("Mission3").Value),
                                        Type3 = int.Parse(xmlNamedNodeMap.GetNamedItem("Type3").Value),
                                        Mission4 = int.Parse(xmlNamedNodeMap.GetNamedItem("Mission4").Value),
                                        Type4 = int.Parse(xmlNamedNodeMap.GetNamedItem("Type4").Value),
                                        EXP = int.Parse(xmlNamedNodeMap.GetNamedItem("EXP").Value),
                                        Points = int.Parse(xmlNamedNodeMap.GetNamedItem("Points").Value),
                                        Item = int.Parse(xmlNamedNodeMap.GetNamedItem("Item").Value),
                                    };
                                    tutorial.Add(mission.Id, mission);
                                }
                            }
                        }
                    }
                    Logger.Info("[TutorialCardSet] Loaded {0} mission card", tutorial.Count);
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
