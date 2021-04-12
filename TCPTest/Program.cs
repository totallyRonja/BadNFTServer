using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

class Server {
    
    private const string handshakeResponse = @"HTTP/1.1 101 Switching Protocols
Connection: Upgrade
Upgrade: websocket
Sec-WebSocket-Accept: {0}

";
    
    public static void Main() {
        string ip = "127.0.0.1";
        int port = 8080;
        var server = new TcpListener(IPAddress.Parse(ip), port);

        server.Start();
        Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection...", ip, port);

        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("A client connected.");

        NetworkStream stream = client.GetStream();

        Handshake(client);

        //todo: actually do stuff
        while (true) {
        }
    }

    static bool Handshake(TcpClient client) {
        var stream = client.GetStream();
        byte[] bytes = new byte[client.Available];
        stream.Read(bytes, 0, client.Available);
        string s = Encoding.UTF8.GetString(bytes);

        if (!s.StartsWith("get", StringComparison.InvariantCultureIgnoreCase)) {
            Console.WriteLine("Handshake failed!");
            return false;
        }

        // 1. Obtain the value of the "Sec-WebSocket-Key" request header without any leading or trailing whitespace
        // 2. Concatenate it with "258EAFA5-E914-47DA-95CA-C5AB0DC85B11" (a special GUID specified by RFC 6455)
        // 3. Compute SHA-1 and Base64 hash of the new value
        // 4. Write the hash back as the value of "Sec-WebSocket-Accept" response header in an HTTP response
        string swk = Regex.Match(s, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
        string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
        byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
        string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

        // HTTP/1.1 defines the sequence CR LF as the end-of-line marker
        byte[] response = Encoding.UTF8.GetBytes(string.Format(handshakeResponse, swkaSha1Base64));

        stream.Write(response, 0, response.Length);

        return true;
    }
}