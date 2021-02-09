using System;
namespace License_Server
{
    public class KeyGen
    {
        // Message Crypto Meme
        private readonly byte[] key = { 0x6A, 0x5D, 0x16, 0x8B, 0x14, 0xE6, 0x4C, 0xAD, 0xD7, 0x0D, 0xA9, 0x34, 0xA0, 0x6C, 0xC2, 0x22 };
        private readonly byte[] iv  = { 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69, 0x69 };
        private readonly Crypto MemeCripto = new Crypto();
        // Keygen
        private readonly string cryptoVocabulary = "jtwIHWJRuzG2oBSsmaQ73Dx6eArPk5LE8VbfnqvhdU9KXYFTgpy4ZMNcC";
        //privateKey es una cadena de numeros random que tambien se puede considerar como una clave privada del server.
        private readonly int[] privateKey = { 
            48, 66, 8, 62, 85, 74, 12, 67, 1, 94, 85, 44, 81, 66, 84, 64, 84, 61, 80, 63, 81, 88, 
            7, 7, 11, 2, 70, 47, 70, 75, 21, 97, 79, 55, 45, 82, 8, 9, 72, 17, 26, 17, 71, 53, 48, 
            11, 72, 47, 97, 92, 67, 52, 3, 48, 0, 31, 54 };

        // Aqui el vocabulario hexadecimal y sus valores, que la id de la gpu los emplea y no vas a calcular con F y C.
        private readonly string hexVocabulary = "0123456789abcdef";
        private readonly int[] hexValues = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

        public byte[] EncryptMessage(byte[] message)
        {
            return MemeCripto.Encrypt(message, key, iv);
        }

        public byte[] DecryptMessage(byte[] message)
        {
            return MemeCripto.Decrypt(message, key, iv);
        }

        private static int SumaDigitos(int suma)
        {
            int sum = 0;
            while (suma > 0 || sum > 9)
            {
                if (suma == 0)
                {
                    suma = sum;
                    sum = 0;
                }
                sum += suma % 10;
                suma /= 10;
            }
            return sum;
        }

        private static char GetLastDigit(string licencia)
        {
            string publicVocabulary = "23456789abcdefghjkmnopqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ";
            int acumuladorLetra = 0;
            for (int c = 0; c < 24; c++)
            {
                acumuladorLetra += publicVocabulary.IndexOf(licencia.ToCharArray()[c]);
            }
            acumuladorLetra = acumuladorLetra % 57;
            return publicVocabulary.ToCharArray()[acumuladorLetra];
        }

        public bool verifyKey(string key, string DeviceID)
        {
            if (!GetLastDigit(key).Equals(key.ToCharArray()[key.Length - 1]))
            {
                Console.WriteLine("Key Incorrecta");
                return false;
            }

            string tmp = GenerateKey(DeviceID);
            if (tmp.Equals(key))
            {
                Console.WriteLine("Key Correcta");
                return true;
            } else
            {
                Console.WriteLine("Key incorrecta");
                return false;
            }
        }

        public string GenerateKey(string DeviceID)
        {
            
            int suma = 69;
            for (int i = 0; i < DeviceID.Length; i++)
            {
                if (i != hexValues[hexVocabulary.IndexOf(DeviceID.ToCharArray()[0])])
                {
                    suma += hexValues[hexVocabulary.IndexOf(DeviceID.ToCharArray()[i])];
                }
            }
            
            int digitoEspecial = SumaDigitos(suma);

            string licencia = "";

            for (int i = 0; i < 24; i++)
            {
                int acumulador = (DeviceID.ToCharArray()[i] * privateKey[i] * digitoEspecial) / DeviceID.ToCharArray()[DeviceID.Length - i - 1];
                acumulador = acumulador % cryptoVocabulary.Length;
                licencia += cryptoVocabulary.ToCharArray()[acumulador];
            }

            licencia += GetLastDigit(licencia);
            return licencia;
        }
    }
}
