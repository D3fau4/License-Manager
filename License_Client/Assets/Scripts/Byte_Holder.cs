using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Byte_Holder
{
    public List<byte> bytes;
    private long remainingLength;

    public Byte_Holder() { }

    public Byte_Holder(long remainingLength)
    {
        this.remainingLength = remainingLength;
        bytes = new List<byte>();
    }

    public void Pad(long padamount)
    {
        for (int i = 0; i < padamount; i++)
        {
            bytes.Add(0);
        }

        remainingLength -= padamount;
    }
    public void Write(byte[] inbytes)
    {
        bytes.AddRange(inbytes.ToList());
        remainingLength -= inbytes.Length;
    }
    public void Write(int i)
    {
        byte[] inbytes = BitConverter.GetBytes(i);
        bytes.AddRange(inbytes.ToList());
        remainingLength -= inbytes.Length;
    }

    public void Write(string s)
    {
        byte[] inbytes = Encoding.ASCII.GetBytes(s);
        bytes.AddRange(inbytes.ToList());
        remainingLength -= inbytes.Length;
    }

    public byte[] DumpToArray()
    {
        return bytes.ToArray();
    }
}

