using System;
using System.Text;
using UnityEngine;

public class MakePackage1
{
    private static int pk11magic = 0x31314B50;
    public byte[] Build()
    {
        Byte_Holder pk11 = new Byte_Holder(0x1024);
        pk11.Write(MakePK11header());
        return pk11.DumpToArray();
    }

    private byte[] MakePK11header()
    {
        Byte_Holder tmp = new Byte_Holder(0x100);
        tmp.Write(pk11magic);
        tmp.Write(SystemInfo.operatingSystem);
        return tmp.DumpToArray();
    }

    public static string GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMddHHmmss");
    }
}
