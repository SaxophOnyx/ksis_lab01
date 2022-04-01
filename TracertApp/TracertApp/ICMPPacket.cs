using System;
using System.Text;

namespace TracertApp
{
    public class ICMPPacket
    {
        private byte[] _header;

        private byte[] _message;

        public byte Type
        {
            get { return _header[0]; }
            set { _header[0] = value; }
        }

        public byte Code
        {
            get { return _header[1]; }
            set { _header[1] = value; }
        }

        public ushort CheckSum
        {
            get { return BitConverter.ToUInt16(_header, 2); }
            //set { BitConverter.GetBytes(value).CopyTo(_header, 2); }
        }

        public ushort Identifier
        {
            get { return BitConverter.ToUInt16(_header, 4); }
            set { BitConverter.GetBytes(value).CopyTo(_header, 4); }
        }

        public ushort SequenceNumber
        {
            get { return BitConverter.ToUInt16(_header, 6); }
            set { BitConverter.GetBytes(value).CopyTo(_header, 6); }
        }

        public byte[] Message
        {
            get { return _message; }
            set { _message = value; }
        }


        public ICMPPacket(byte[] frame, int totalLen)
        {
            _header = new byte[8];
            Buffer.BlockCopy(frame, 20, _header, 0, 8);

            _message = new byte[totalLen - 20 - 8];
            Buffer.BlockCopy(frame, 28, _message, 0, _message.Length);
        }

        public ICMPPacket()
        {
            _header = new byte[8];
            Type = 8;
            Code = 0;
            Identifier = 111;
            SequenceNumber = 0;
            _message = Encoding.Unicode.GetBytes("ABCD");

            ushort checkSum = this.CalcCheckSum();
            BitConverter.GetBytes(checkSum).CopyTo(_header, 2);

        }


        public byte[] CreateByteArray()
        {
            byte[] res = new byte[_header.Length + _message.Length];

            _header.CopyTo(res, 0);
            _message.CopyTo(res, _header.Length);

            return res;
        }

        public ushort CalcCheckSum()
        {
            uint res = 0;

            for (int i = 0; i < _header.Length; i += 2)
                res += Convert.ToUInt32(BitConverter.ToUInt16(_header, i));

            for (int i = 0; i < _message.Length; i += 2)
                res += Convert.ToUInt32(BitConverter.ToUInt16(_message, i));

            res = (res >> 16) + (res & 0xffff);
            res += (res >> 16);

            return (ushort)(~res);
        }

        public void Print()
        {
            for (int i = 0; i < _header.Length; ++i)
            {
                Console.Write(" {0, 3} ", _header[i]);
                if ((i + 1) % 4 == 0)
                    Console.WriteLine();
            }

            Console.WriteLine("======================");

            for (int i = 0; i < _message.Length; ++i)
            {
                Console.Write(" {0, 3} ", _message[i]);
                if ((i + 1) % 4 == 0)
                    Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
