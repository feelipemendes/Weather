using Microsoft.AspNetCore.Mvc;
using Weather.Server.Dtos.OpenWeatherMap;
using Weather.Server.Services.Interfaces;

namespace Wheater.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IOpenWeatherMapServices _openWeatherMapServices;

		public WeatherForecastController(
			ILogger<WeatherForecastController> logger,
			IOpenWeatherMapServices openWeatherMapServices)
		{
			_logger = logger;
			_openWeatherMapServices = openWeatherMapServices;
		}

		[HttpGet(Name = "GetCoordinatesByLocationName")]
		public Task<ObjectResult> GetCoordinatesByLocationName([FromBody] CoordinatesByLocationNameRequestDto locationName)
		{
			
			var result = _openWeatherMapServices.GetCoordinatesByLocationName(locationName);

		}
	}
}
