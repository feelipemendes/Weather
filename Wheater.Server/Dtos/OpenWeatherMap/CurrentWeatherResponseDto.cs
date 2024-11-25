using Newtonsoft.Json;

namespace Weather.Server.Dtos.OpenWeatherMap
{
	public class CurrentWeatherResponseDto
	{
		//[JsonProperty("coord")]
		//public Coord coord { get; set; }

		[JsonProperty("weather")]
		public List<Weather> weather { get; set; }

		//[JsonProperty("base")]
		//public string @base { get; set; }

		[JsonProperty("main")]
		public Main main { get; set; }

		[JsonProperty("visibility")]
		public int visibility { get; set; }

		//[JsonProperty("wind")]
		//public Wind wind { get; set; }

		//[JsonProperty("rain")]
		//public Rain rain { get; set; }

		//[JsonProperty("clouds")]
		//public Clouds clouds { get; set; }

		//[JsonProperty("dt")]
		//public int dt { get; set; }

		//[JsonProperty("sys")]
		//public Sys sys { get; set; }

		//[JsonProperty("timezone")]
		//public int timezone { get; set; }

		[JsonProperty("id")]
		public int id { get; set; }

		[JsonProperty("name")]
		public string name { get; set; }

		//[JsonProperty("cod")]
		//public int cod { get; set; }
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

		//[JsonProperty("sea_level")]
		//public int sea_level { get; set; }

		//[JsonProperty("grnd_level")]
		//public int grnd_level { get; set; }
	}

	public class Weather
	{
		//[JsonProperty("id")]
		//public int id { get; set; }

		[JsonProperty("main")]
		public string main { get; set; }

		[JsonProperty("description")]
		public string description { get; set; }

		//[JsonProperty("icon")]
		//public string icon { get; set; }
	}
}
