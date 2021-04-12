using System.IO;

public static class Page {
	private static string baseSite;

	public static string GetContent() {
		baseSite ??= File.ReadAllText("Website.html");
		baseSite = baseSite.Replace("{{name}}", Database.GetName());
		return baseSite;
	}
}