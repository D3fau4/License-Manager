using System;
using System.Collections.Generic;
using System.Text;

namespace License_Server
{
    public class KeyGen
    {
        private byte[] key = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private byte[] iv = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        Crypto MemeCripto = new Crypto();
        public byte[] EncryptMessage(byte[] message)
        {
            return MemeCripto.Encrypt(message, key, iv);
        }

        public byte[] DecryptMessage(byte[] message)
        {
            return MemeCripto.Decrypt(message, key, iv);
        }
    }
}
