public class Database {
	private static string name = "Ronja";

	public static string GetName() {
		return name;
	}
	
	public static void SetName(string name) {
		Database.name = name;
	}
}