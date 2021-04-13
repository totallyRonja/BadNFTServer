using System.IO;
using System.Net;
using System.Threading.Tasks;
using System;

public static class Page {
	private static string baseSite;

	public static string GetContent() {
		baseSite ??= File.ReadAllText("Website.html");
		var site = baseSite.Replace("{{name}}", Database.GetName());
		return site;
	}

	public static async Task SendImage(HttpListenerContext context) {
		var response = context.Response;
		var request = context.Request;
		if (request.RawUrl == "/Orb_Off.png") {
			Memory<byte> data = await File.ReadAllBytesAsync("Orb_Off.png");
			await response.OutputStream.WriteAsync(data);
			response.Close();
		}
		if (request.RawUrl == "/Orb_On.png") {
			Memory<byte> data = await File.ReadAllBytesAsync("Orb_On.png");
			await response.OutputStream.WriteAsync(data);
			response.Close();
		}
		if (request.RawUrl == "/NFT.png") {
			Memory<byte> data = await File.ReadAllBytesAsync("NFT.png");
			await response.OutputStream.WriteAsync(data);
			response.Close();
		}
	}
}