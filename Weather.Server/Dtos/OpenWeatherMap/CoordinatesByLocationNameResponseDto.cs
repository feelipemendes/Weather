using System.Text.Json.Serialization;

namespace Weather.Server.Dtos.OpenWeatherMap
{
	public class CoordinatesByLocationNameResponseDto
	{
		[JsonPropertyName("name")]
		public string name { get; set; }

		[JsonPropertyName("lat")]
		public double lat { get; set; }

		[JsonPropertyName("lon")]
		public double lon { get; set; }

		[JsonPropertyName("country")]
		public string country { get; set; }

		[JsonPropertyName("state")]
		public string state { get; set; }
	}
}
