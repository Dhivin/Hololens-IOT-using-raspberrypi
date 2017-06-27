using UnityEngine; 
using System;
using System.IO;
using System.Net.Sockets;


public class networkSocket : MonoBehaviour
{


    public String host = "172.16.4.181";
    public Int32 port = 8080;
    int toggleFlag = 0;
    internal Boolean socket_ready = false;
    internal String input_buffer = "Lighton\n";
    private TcpClient tcp_socket;
    NetworkStream net_stream;

    StreamWriter socket_writer;
    StreamReader socket_reader;



    void Update()
    {
        string received_data = readSocket();

        // writeSocket(input_buffer);


        if (received_data != "")
        {
            // Do something with the received data,
            // print it in the log for now
            Debug.Log(received_data);
        }
    }


    void Awake()
    {
        toggleFlag = 0;
        setupSocket();

    }
    void start()
    {

    }
    void OnApplicationQuit()
    {
        closeSocket();
    }

    public void setupSocket()
    {
        try
        {
            tcp_socket = new TcpClient(host, port);

            net_stream = tcp_socket.GetStream();
            socket_writer = new StreamWriter(net_stream);
            socket_reader = new StreamReader(net_stream);

            socket_ready = true;
        }
        catch (Exception e)
        {
            // Something went wrong
            Debug.Log("Socket error: " + e);
        }
    }

    public void writeSocket(string line)
    {
        if (!socket_ready)
            return;

        line = line + "\r\n";
        socket_writer.Write(line);
        socket_writer.Flush();
    }
    public void switchOnLight()
    {
        if (toggleFlag % 2 == 0)
        {
            input_buffer = "Lighton\n";
            writeSocket(input_buffer);
            toggleFlag = toggleFlag + 1;
        }
        else
        {

            input_buffer = "Lightoff\n";
            writeSocket(input_buffer);
            toggleFlag = toggleFlag + 1;

        }

    }
    public String readSocket()
    {
        if (!socket_ready)
            return "";

        if (net_stream.DataAvailable)
            return socket_reader.ReadLine();

        return "";
    }

    public void closeSocket()
    {
        if (!socket_ready)
            return;

        socket_writer.Close();
        socket_reader.Close();
        tcp_socket.Close();
        socket_ready = false;
    }

}

