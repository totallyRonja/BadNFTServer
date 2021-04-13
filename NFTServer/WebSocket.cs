using System.Threading.Tasks;
using WebSocketSharp.Server;

public class WebSocket {
	public bool Running = false;
	private WebSocketServer server;

	public WebSocket() {
		server = new WebSocketServer("ws://192.168.178.35:3002");
		server.AddWebSocketService<UpdateProvider>("/updates");
	}
	
	public void Start() {
		Running = true;
		server.Start();
	}
	
	public void Stop() {
		Running = false;
		server.Stop();
	}
}