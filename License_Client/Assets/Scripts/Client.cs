using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour
{
    private readonly byte[] message;
    private readonly MakePackage1 package1 = new MakePackage1();
    [SerializeField]
    private string IP;
    [SerializeField]
    private int Port;
    [SerializeField]
    private bool Encrypt = true;
    [SerializeField]
    private GameObject DeviceID;
    private bool Generate = true;
    private byte[] pk11;

    // Start is called before the first frame update
    private void Start()
    {
        Socket Cliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("Conectando con: " + IP);
        Cliente.Connect(IP, Port);
        try
        {
            Debug.Log("Contruyendo Package1");
            pk11 = package1.Build("123", Encrypt, Generate);
            int meme = Cliente.Send(pk11);
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
        DeviceID.GetComponent<UnityEngine.UI.Text>().text = SystemInfo.deviceUniqueIdentifier;
    }
}
