using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace License_Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            KeyGen Decriptor = new KeyGen();
            TcpListener server = new TcpListener(IPAddress.Parse("0.0.0.0"), 6969);

            server.Start();
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            Console.WriteLine("Se ha conectado el cliente.");

            while (true)
            {
                while (stream.DataAvailable == true)
                {
                    Console.WriteLine("Datos disponibles");
                    byte[] bytes = new byte[client.Available];
                    Console.WriteLine("Leyendo paquete");
                    stream.Read(bytes, 0, bytes.Length);
                    File.WriteAllBytes("pk11.bin", bytes);
                    MemoryStream output = new MemoryStream(bytes);
                    BinaryReader Reader = new BinaryReader(output);
                    // Leyendo Cabecera
                    byte[] Header = new byte[0x4];
                    Reader.Read(Header, 0, Header.Length);
                    // Veridicando magic
                    if (Encoding.ASCII.GetString(Header) == "PK11")
                    {
                        Console.WriteLine("Cabecera correcta");
                        int IsCripted, Gen, size;
                        IsCripted = Reader.ReadInt32();
                        Gen = Reader.ReadInt32();
                        size = Reader.ReadInt32();
                        Console.WriteLine("Generar: " + Gen);
                        Console.WriteLine("IsCripted: " + IsCripted);
                        Console.WriteLine("Size: " + size);
                        // Comprobar si es encriptado o no, en caso de que si desencriptar
                        if (IsCripted == 0x1)
                        {
                            Console.WriteLine("Mensaje encriptado: " + Encoding.ASCII.GetString(bytes));
                            byte[] dec = Decriptor.DecryptMessage(Reader.ReadBytes(size));
                            Console.WriteLine("Mensaje desencriptado: " + Encoding.ASCII.GetString(dec));
                            MemoryStream outputdec = new MemoryStream(dec);
                            BinaryReader Readerdec = new BinaryReader(outputdec);
                            string timestamp = Encoding.ASCII.GetString(Readerdec.ReadBytes(0x8));
                            if (timestamp == GetTimestamp(DateTime.Now))
                            {
                                Console.WriteLine("TimeStamp correcto: " + timestamp);
                            }
                        }
                        else
                        {
                            Console.WriteLine(Encoding.ASCII.GetString(bytes));
                            byte[] timestamp = new byte[0x8];
                            Reader.Read(timestamp, 0, timestamp.Length);
                            if (Encoding.ASCII.GetString(timestamp) == GetTimestamp(DateTime.Now))
                            {
                                Console.WriteLine("TimeStamp correcto: " + Encoding.ASCII.GetString(timestamp));
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cabecera Incorrecta");
                    }
                }
            }
        }
        private static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMdd");
        }
    }
}
