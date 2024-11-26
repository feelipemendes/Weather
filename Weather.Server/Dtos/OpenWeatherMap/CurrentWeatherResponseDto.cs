using Newtonsoft.Json;

namespace Weather.Server.Dtos.OpenWeatherMap
{
	public class CurrentWeatherResponseDto
	{
		[JsonProperty("weather")]
		public List<Weather> weather { get; set; }

		[JsonProperty("main")]
		public Main main { get; set; }

		[JsonProperty("visibility")]
		public int visibility { get; set; }

		[JsonProperty("id")]
		public int id { get; set; }

		[JsonProperty("name")]
		public string name { get; set; }
	}

	public class Main
	{
		[JsonProperty("temp")]
		public double temp { get; set; }

		[JsonProperty("feels_like")]
		public double feels_like { get; set; }

		[JsonProperty("temp_min")]
		public double temp_min { get; set; }

		[JsonProperty("temp_max")]
		public double temp_max { get; set; }

		[JsonProperty("pressure")]
		public int pressure { get; set; }

		[JsonProperty("humidity")]
		public int humidity { get; set; }
	}

	public class Weather
	{

		[JsonProperty("main")]
		public string main { get; set; }

		[JsonProperty("description")]
		public string description { get; set; }
	}
}
