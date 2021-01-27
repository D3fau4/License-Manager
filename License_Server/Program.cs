using System;
using System.Net;
using System.Net.Sockets;

namespace License_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

            server.Start();
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Se ha conectado el cliente.");
        }
    }
}
