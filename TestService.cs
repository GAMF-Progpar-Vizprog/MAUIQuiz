using System.Text.Json;

namespace MAUIQuiz
{
	public class TestService
	{
		// Egyetlen, megosztott HttpClient: nem hozunk létre minden kéréshez újat
		static readonly HttpClient client = new() { Timeout = TimeSpan.FromSeconds(20) };

		public static async Task<List<Item>> GetTest(string Url, int n, int langcode)
		{
			//quiz.php?n=5&lang=hu
			var query = $"{Url}?n={n}&lang={(langcode == 0 ? "en" : "hu")}";
			var json = await client.GetStringAsync(query);
			return JsonSerializer.Deserialize<List<Item>>(json) ?? [];
		}
	}
}
