using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Managers
{
    public class MapsManager
    {
        public static Dictionary<byte, Map> maps;

        public static void Run()
        {
            maps = new Dictionary<byte, Map>();
        }
    }
}
