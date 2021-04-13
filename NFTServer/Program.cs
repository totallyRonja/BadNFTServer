using System;
using System.Threading.Tasks;

class Program
{
    const string url = "http://*:80/";
    static async Task Main(string[] args) {
        var server = new Server(url);
        server.Start();
        var serverTask = server.Listen();

        var socket = new WebSocket();
        socket.Start();
        
        await serverTask;
        
        server.Stop();
        socket.Stop();
    }
}
