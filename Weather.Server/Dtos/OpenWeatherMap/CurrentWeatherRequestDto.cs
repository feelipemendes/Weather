namespace Weather.Server.Dtos.OpenWeatherMap
{
	public class CurrentWeatherRequestDto
	{
		public double lat { get; set; }
		public double lon { get; set; }
        public string LocationName { get; set; }
    }
}
