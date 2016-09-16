/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Core.Network
{
    public class HexToByte
    {
        public static byte[] Convert(string hexString)
        {
            if (hexString.Length % 2 == 0)
                ;
            byte[] numArray = new byte[hexString.Length / 2];
            for (int index = 0; index < numArray.Length; ++index)
            {
                string s = hexString.Substring(index * 2, 2);
                numArray[index] = byte.Parse(s, NumberStyles.HexNumber, (IFormatProvider)CultureInfo.InvariantCulture);
            }
            return numArray;
        }
    }
}
