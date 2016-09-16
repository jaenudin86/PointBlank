/*
 * C# Server Emulator Project Blackout / PointBlank
 * Authors: the__all
 * Copyright (C) 2015 | OZ-Network
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointBlank.Network
{
    public abstract class ReceivePacket
    {
        private byte[] _buffer;
        private int _offset;
        private Auth _Client;

        protected internal Auth getClient()
        {
            return _Client;
        }

        protected internal byte[] getBuffer()
        {
            return _buffer;
        }

        protected internal void makeme(Auth Client, byte[] buffer)
        {
            _Client = Client;
            _buffer = buffer;
            _offset = 2;
            ReadImpl();
        }

        protected internal int ReadD()
        {
            int res = BitConverter.ToInt32(_buffer, _offset);
            _offset += 4;
            return res;
        }

        protected internal byte ReadC()
        {
            byte res = _buffer[_offset];
            _offset += 1;
            return res;
        }

        protected internal byte[] ReadB(int Length)
        {
            byte[] result = new byte[Length];
            Array.Copy(_buffer, _offset, result, 0, Length);
            _offset += Length;
            return result;
        }

        protected internal short ReadH()
        {
            short res = BitConverter.ToInt16(_buffer, _offset);
            _offset += 2;
            return res;
        }

        protected internal double ReadF()
        {
            double res = BitConverter.ToDouble(_buffer, _offset);
            _offset += 8;
            return res;
        }

        protected internal long ReadQ()
        {
            long res = BitConverter.ToInt64(_buffer, _offset);
            _offset += 8;
            return res;
        }

        protected internal string ReadS(int Length)
        {
            string res = "";
            try
            {
                res = Encoding.GetEncoding(1251).GetString(_buffer, _offset, Length);

                int idx = res.IndexOf((char)0x00);
                if (idx != -1)
                {
                    res = res.Substring(0, idx);
                }
                _offset += Length;
            }
            catch (Exception ex)
            {
                //CLogger.error("while reading string from packet, " + ex.Message + " " + ex.StackTrace);
            }
            return res;
        }

        protected internal string ReadS()
        {
            string result = "";
            try
            {
                int count = (_buffer.Length - _offset);
                result = System.Text.Encoding.Unicode.GetString(_buffer, _offset, count);

                int idx = result.IndexOf((char)0x00);
                if (idx != -1)
                {
                    result = result.Substring(0, idx);
                }
                _offset += result.Length + 1;
            }
            catch (Exception ex)
            {
                //CLogger.error("while reading string from packet, " + ex.Message + " " + ex.StackTrace);
            }
            return result;
        }

        protected internal void ignore(int in_offset)
        {
            _offset = _offset + in_offset;
            Console.WriteLine("Ignore " + in_offset + " bytes");
        }

        public abstract void ReadImpl();
        public abstract void RunImpl();
    }
}
