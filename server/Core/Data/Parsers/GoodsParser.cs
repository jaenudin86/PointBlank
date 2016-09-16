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
    public class GoodsParser
    {
        public static Dictionary<int, Good> goods = new Dictionary<int, Good>();

        public static void Load()
        {
            String path = "Data//Goods.xml";
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
                                if ("Item".Equals(xmlNode2.Name))
                                {
                                    XmlNamedNodeMap xmlNamedNodeMap = xmlNode2.Attributes;
                                    Good good = new Good
                                    {
                                        good_id = int.Parse(xmlNamedNodeMap.GetNamedItem("Id").Value),
                                        item_id = int.Parse(xmlNamedNodeMap.GetNamedItem("Item").Value),
                                        priceCredits = int.Parse(xmlNamedNodeMap.GetNamedItem("Credits").Value),
                                        pricePoints = int.Parse(xmlNamedNodeMap.GetNamedItem("Points").Value),
                                        quantity = int.Parse(xmlNamedNodeMap.GetNamedItem("Quantity").Value),
                                        type = int.Parse(xmlNamedNodeMap.GetNamedItem("Type").Value),
                                        stockType = int.Parse(xmlNamedNodeMap.GetNamedItem("StockType").Value),
                                        ItemType = int.Parse(xmlNamedNodeMap.GetNamedItem("ItemType").Value),
                                    };

                                    goods.Add(good.getGoodId(), good);
                                }
                            }
                        }
                    }
                    Logger.Info("[Shop] Loaded {0} goods", goods.Count);
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
