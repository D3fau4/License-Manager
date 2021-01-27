using System;
using System.IO;
using UnityEngine;

public class MakePackage1
{

    public byte[] Build()
    {
        byte[] array = new byte[0x4];
        MemoryStream output = new MemoryStream(array);
        using (BinaryWriter binaryWriter = new BinaryWriter(output))
        {
            binaryWriter.Write(MakePK11header());
            Debug.Log("Total Package1 size: " + array.Length);
            return array;
        }
    }

    private byte[] MakePK11header()
    {
        byte[] array = new byte[0x4];
        MemoryStream output = new MemoryStream(array);
        BinaryWriter binaryWriter = new BinaryWriter(output);
        binaryWriter.Write(0x31314B50);
        return array;
    }

    public static string GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMddHHmmss");
    }
}
