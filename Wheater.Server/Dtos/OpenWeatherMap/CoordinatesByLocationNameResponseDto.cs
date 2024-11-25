using Newtonsoft.Json;

namespace Weather.Server.Dtos.OpenWeatherMap
{
	public class CoordinatesByLocationNameResponseDto
	{
		[JsonProperty("name")]
		public string name { get; set; }

		[JsonProperty("local_names")]
		public LocalNames local_names { get; set; }

		[JsonProperty("lat")]
		public double lat { get; set; }

		[JsonProperty("lon")]
		public double lon { get; set; }

		[JsonProperty("country")]
		public string country { get; set; }

		[JsonProperty("state")]
		public string state { get; set; }
	}
	public class LocalNames
	{
		[JsonProperty("en")]
		public string en { get; set; }
	}
}
