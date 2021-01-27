using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    private readonly byte[] message;
    private readonly MakePackage1 package1 = new MakePackage1();
    private string Host;

    // Start is called before the first frame update
    private void Start()
    {
        Host = "127.0.0.1";
        Socket Cliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("Conectando con: " + Host);
        Cliente.Connect(Host, 80);
        try
        {
            byte[] msg = Encoding.ASCII.GetBytes("PK11");
            int meme = Cliente.Send(msg);
            Debug.Log(Encoding.UTF8.GetString(msg));
            Debug.Log(meme);
            Debug.Log("Conectado");
        }
        catch (System.Exception)
        {
            Debug.LogError("dsfsd");
            throw;
        }

    }

    public static byte[] ReadFully(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
