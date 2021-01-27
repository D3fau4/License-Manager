using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace License_Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

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
                    stream.Read(bytes, 0, bytes.Length);
                    string data = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine(data);
                }
            }
        }
    }
}
