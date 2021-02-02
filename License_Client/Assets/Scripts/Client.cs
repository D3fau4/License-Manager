using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    private readonly byte[] message;
    private readonly MakePackage1 package1 = new MakePackage1();
    public string IP;
    public int Port;
    private byte[] pk11;

    // Start is called before the first frame update
    private void Start()
    {
        Socket Cliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("Conectando con: " + IP);
        Cliente.Connect(IP, Port);
        try
        {
            pk11 = package1.Build("123", true, true);
            int meme = Cliente.Send(pk11);
            Debug.Log(meme);
            Debug.Log("Conectado");
        }
        catch (System.Exception)
        {
            Debug.LogError("dsfsd");
            throw;
        }

    }

    // Update is called once per frame
    private void Update()
    {

    }
}
