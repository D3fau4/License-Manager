﻿namespace License_Server
{
    public class KeyGen
    {
        private readonly byte[] key = { 0x6A, 0x5D, 0x16, 0x8B, 0x14, 0xE6, 0x4C, 0xAD, 0xD7, 0x0D, 0xA9, 0x34, 0xA0, 0x6C, 0xC2, 0x22 };
        private readonly byte[] iv  = { 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69 };
        private readonly Crypto MemeCripto = new Crypto();
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
