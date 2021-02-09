using System;

namespace License_Server
{
    internal class MakePackage1
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

            Byte_Holder tmp = new Byte_Holder(0x6);
            tmp.Write(pk11magic);
            if (enc)
            {
                tmp.Write(0x1); // Encriptado
            }
            else
            {
                tmp.Write(0x0); // No Encriptado
            }
            if (gen)
            {
                tmp.Write(0x2); // Recibir
            }
            else
            {
                tmp.Write(0x3); // verificado
            }

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
    }
}
