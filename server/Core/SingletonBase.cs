/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public abstract class SingletonBase<T> where T : class, new()
    {
        public static T _instance = new T();

        public static T Load()
        {
            return _instance;
        }
    }
}