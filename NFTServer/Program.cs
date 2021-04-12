using System;
using System.Threading.Tasks;

class Program
{
    const string url = "http://localhost:8000/";
    static async Task Main(string[] args) {
        var server = new Server(url);
        server.Start();
        var serverTask = server.Listen();

        var socket = new WebSocket();
        socket.Start();
        var socketTask = socket.Listen();
        
        await serverTask;
        await socketTask;
        
        server.Stop();
        socket.Stop();
    }
}
