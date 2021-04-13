using System;
using System.Diagnostics;
using WebSocketSharp;
using WebSocketSharp.Server;

public class UpdateProvider : WebSocketBehavior {
	static ProfanityFilter.ProfanityFilter filter = new();
	
	protected override void OnMessage(MessageEventArgs e) {
		var text = e.Data;
		if (text.Length > 280)
			text = text.Substring(0, 280);
		//if(filter.ContainsProfanity(text))
		//	return;
		text = text.Replace("<", "&lt;");
		text = text.Replace(">", "&gt;");
		
		if(State != WebSocketState.Open)
			return;

		Database.SetName(text);
	}

	protected override void OnOpen() {
		Database.NameChanged += SendUpdate;
	}

	protected override void OnClose(CloseEventArgs e) {
		Database.NameChanged -= SendUpdate;
	}

	private void SendUpdate(string name) {
		Send(name);
	}
}