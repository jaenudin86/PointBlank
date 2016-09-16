/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Core.Managers
{
    public class ThreadManager
    {
        public void executeTask(Thread t)
        {
            t.Start();
        }
    }
}
