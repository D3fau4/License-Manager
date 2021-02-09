using System.Net.Sockets;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class Client : MonoBehaviour
{
    private readonly byte[] message;
    private MakePackage1 package1 = new MakePackage1();
    [SerializeField]
    private string IP;
    [SerializeField]
    private int Port;
    [SerializeField]
    private bool Encrypt = true;
    [SerializeField]
    private GameObject DeviceID;
    [SerializeField]
    private GameObject License;
    [SerializeField]
    private UnityEngine.UI.Button GenButton;
    [SerializeField]
    private UnityEngine.UI.Button VerButton;
    [SerializeField]
    private GameObject licenseTextBox;
    private bool Generate = true;
    private byte[] pk11;
    private Socket Cliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private KeyGen Decriptor = new KeyGen();
    private string key;

    // Start is called before the first frame update
    private void Start()
    {
        DeviceID.GetComponent<UnityEngine.UI.Text>().text += SystemInfo.deviceUniqueIdentifier;
        Debug.Log("Conectando con: " + IP);
        Cliente.Connect(IP, Port);
        GenButton.onClick.AddListener(delegate () { SendGenMessage(); });
        VerButton.onClick.AddListener(delegate () { SendValidMessage(); });
    }

    private void SendGenMessage()
    {
        try
        {
            Debug.Log("Contruyendo Package1");
            pk11 = package1.Build(SystemInfo.deviceUniqueIdentifier, Encrypt, true);
            int meme = Cliente.Send(pk11);
            Debug.Log("Conectado");
        }
        catch (System.Exception)
        {
            Debug.LogError("dsfsd");
            throw;
        }
    }

    private void SendValidMessage()
    {
        try
        {
            Debug.Log("Contruyendo Package1");
            pk11 = package1.Build(key + SystemInfo.deviceUniqueIdentifier, Encrypt, false);
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
        key = licenseTextBox.GetComponent<UnityEngine.UI.Text>().text;
        if (Cliente.Available != 0)
        {
            byte[] tmp = new byte[Cliente.Available];
            Debug.Log("Tamaño de paquete recibido: " + Cliente.Available);
            Cliente.Receive(tmp);
            MemoryStream output = new MemoryStream(tmp);
            BinaryReader Reader = new BinaryReader(output);
            // Leyendo Cabecera
            byte[] Header = new byte[0x4];
            Reader.Read(Header, 0, Header.Length);
            if (Encoding.ASCII.GetString(Header) == "PK11")
            {
                Debug.Log("Header correcto");
                int IsCripted, Gen, size;
                IsCripted = Reader.ReadInt32();
                Gen = Reader.ReadInt32();
                size = Reader.ReadInt32();
                Debug.Log(IsCripted);
                Debug.Log(Gen);
                Debug.Log(size);
                if (IsCripted == 0x1)
                {
                    byte[] dec = Decriptor.DecryptMessage(Reader.ReadBytes(size));
                    MemoryStream outputdec = new MemoryStream(dec);
                    BinaryReader Readerdec = new BinaryReader(outputdec);
                    string timestamp = Encoding.ASCII.GetString(Readerdec.ReadBytes(0x8));
                    if (timestamp == package1.GetTimestamp(DateTime.Now))
                    {
                        Debug.Log("TimeStamp Correcto");
                        if (Gen == 0x2)
                        {
                            string messageDevice = Encoding.ASCII.GetString(Readerdec.ReadBytes(size));
                            Debug.Log(messageDevice);
                            License.GetComponent<UnityEngine.UI.Text>().text = "License: " + messageDevice;
                        }
                        else if (Gen == 0x3)
                        {
                            string messageDevice = Encoding.ASCII.GetString(Readerdec.ReadBytes(0x2));
                            Debug.Log(messageDevice);
                            if (messageDevice.Equals("OK") == true)
                            {
                                Debug.Log("Key Correcta");
                                License.GetComponent<UnityEngine.UI.Text>().text = "License: Valid key";
                            }
                            else
                            {
                                Debug.Log("Key no Correcta");
                                License.GetComponent<UnityEngine.UI.Text>().text = "License: invalid key";
                            }
                        }
                    }
                }
                else
                {
                    byte[] dec = Reader.ReadBytes(size);
                    MemoryStream outputdec = new MemoryStream(dec);
                    BinaryReader Readerdec = new BinaryReader(outputdec);
                    string timestamp = Encoding.ASCII.GetString(Readerdec.ReadBytes(0x8));
                    if (timestamp == package1.GetTimestamp(DateTime.Now))
                    {
                        Debug.Log("TimeStamp Correcto");
                        if (Gen == 0x2)
                        {
                            string messageDevice = Encoding.ASCII.GetString(Readerdec.ReadBytes(size));
                            Debug.Log(messageDevice);
                            License.GetComponent<UnityEngine.UI.Text>().text += messageDevice;
                        }
                        else if (Gen == 0x3)
                        {
                            string messageDevice = Encoding.ASCII.GetString(Readerdec.ReadBytes(0x2));
                            Debug.Log(messageDevice);
                            if (messageDevice.Equals("OK") == true)
                            {
                                Debug.Log("Key Correcta");
                                License.GetComponent<UnityEngine.UI.Text>().text = "License: Valid key";
                            }
                            else
                            {
                                Debug.Log("Key no Correcta");
                                License.GetComponent<UnityEngine.UI.Text>().text = "License: invalid key";
                            }
                        }
                    }
                }
            }
        }
    }
}
