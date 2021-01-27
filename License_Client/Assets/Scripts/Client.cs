using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour
{
    string Host;
    // Start is called before the first frame update
    void Start()
    {
        Host = "127.0.0.1";
        Socket Cliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("Conectando con: " + Host);
        Cliente.Connect(Host, 80);
        Debug.Log("Conectado");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
