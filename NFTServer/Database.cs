using System;

public class Database {
	private static string name = "Ronja";
	public static Action<string> NameChanged;

	public static string GetName() {
		return name;
	}
	
	public static void SetName(string name) {
		Database.name = name;
		NameChanged(name);
	}
}