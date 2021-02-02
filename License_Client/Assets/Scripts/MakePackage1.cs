using System;
using System.Security.Cryptography;
using UnityEngine;

public class MakePackage1
{
    private static readonly int pk11magic = 0x31314B50;
    public byte[] Build(string id, bool enc = false, bool gen = true)
    {
        KeyGen meme = new KeyGen();
        Byte_Holder pk11 = new Byte_Holder(0x1024);
        pk11.Write(MakePK11header(enc, gen));
        if (enc)
        {
            pk11.Write(meme.EncryptMessage(MakeBody(0xff, id)));
        }
        else
        {
            pk11.Write(MakeBody(0xff, id));
        }

        return pk11.DumpToArray();
    }

    private byte[] MakePK11header(bool enc, bool gen)
    {
        Debug.Log("Contruyendo Header");
        Byte_Holder tmp = new Byte_Holder(0x6);
        tmp.Write(pk11magic);
        if (enc)
        {
            Debug.Log("Encriptado");
            tmp.Write(0x1); // Encriptado
        }
        else
        {
            Debug.Log("No Encriptado");
            tmp.Write(0x0); // No Encriptado
        }
        if (gen)
        {
            Debug.Log("Generar");
            tmp.Write(0x1); // Generar
        }
        else
        {
            Debug.Log("No Generar");
            tmp.Write(0x0); // Verificar
        }
        Debug.Log("Tamaño del cuerpo: " + 0xff);
        tmp.Write(0xff);
        return tmp.DumpToArray();
    }

    private byte[] MakeBody(int size, string id)
    {
        Byte_Holder tmp = new Byte_Holder(size);
        tmp.Write(GetTimestamp(DateTime.Now));
        tmp.Write(id);
        return tmp.DumpToArray();
    }

    public static string GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMdd");
    }

    private byte[] hash(byte[] file)
    {
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] temp = sha256Hash.ComputeHash(file);
            byte[] bytes = new byte[4];
            bytes[0] = temp[0];
            bytes[1] = temp[1];
            bytes[2] = temp[2];
            bytes[3] = temp[3];
            return bytes;
        }
    }
}
