namespace Weather.Server.Dtos.OpenWeatherMap
{
	public class CurrentWeatherDto
	{
		public double lat { get; set; }
		public double lon { get; set; }
        public string LocationName { get; set; }
    }
}
