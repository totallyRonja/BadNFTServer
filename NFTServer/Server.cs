using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
public class Server {
	public bool Running;
	private readonly HttpListener listener;

	public Server(string url) {
		listener = new HttpListener();
		listener.Prefixes.Add(url);
	}

	public void Start() {
		Running = true;
		listener.Start();
		Console.WriteLine($"Listening to connections at: {string.Join(", ", listener.Prefixes)}");
	}

	public async Task Listen() {
		while (Running && listener.IsListening) {
			var context = await listener.GetContextAsync();
			var request = context.Request;
			var response = context.Response;

			if (request.Url?.AbsolutePath == "/")
				await RespondPage(response);
			else
				await Page.SendImage(context);
		}
	}

	private async Task RespondPage(HttpListenerResponse response) {
		byte[] data = Encoding.UTF8.GetBytes(Page.GetContent());
		response.ContentType = "text/html";
		response.ContentEncoding = Encoding.UTF8;
		response.ContentLength64 = data.LongLength;

		await response.OutputStream.WriteAsync(data.AsMemory(0, data.Length));
		response.Close();
	}

	public void Stop() {
		Running = false;
		listener.Close();
	}
}