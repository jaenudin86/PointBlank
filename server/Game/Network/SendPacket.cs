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

namespace Game.Network
{
    public abstract class SendPacket
    {
        private MemoryStream mstream = new MemoryStream();
        public static int UNBUFFERED_STATE = 353;

        protected internal void WriteB(byte[] value)
        {
            mstream.Write(value, 0, value.Length);
        }
        protected internal void WriteD(int value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteH(short val)
        {
            WriteB(BitConverter.GetBytes(val));
        }

        protected internal void WriteC(byte value)
        {
            mstream.WriteByte(value);
        }

        protected internal void WriteF(double value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteQ(long value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteS(string value)
        {
            if (value != null)
                WriteB(Encoding.Unicode.GetBytes(value));

            WriteH(0);
        }

        protected internal void WriteS(string name, int count)
        {
            if (name != null)
            {
                WriteB(Encoding.GetEncoding(1251).GetBytes(name));
                WriteB(new byte[count - name.Length]);
            }
        }

        public byte[] ToByteArray()
        {
            return mstream.ToArray();
        }

        public long Length
        {
            get { return mstream.Length; }
        }

        public abstract void WriteImpl();
    }
}
