using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace License_Server
{
    internal class Program
    {
        StreamReader package1;
        private static void Main(string[] args)
        {
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
                    MemoryStream output = new MemoryStream(bytes);
                    BinaryReader Reader = new BinaryReader(output);
                    // Leyendo Cabecera
                    byte[] Header = new byte[0x4];
                    Reader.Read(Header, 0, Header.Length);

                    if (Encoding.ASCII.GetString(Header) == "PK11")
                    {
                        Console.WriteLine("Cabecera correcta");
                    }
                    else
                    {
                        Console.WriteLine("Cabecera Incorrecta");
                    }
                }
            }
        }
    }
}
