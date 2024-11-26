using Microsoft.AspNetCore.Mvc;
using Weather.Server.Dtos.OpenWeatherMap;
using Weather.Server.Entities;
using Weather.Server.Services.Interfaces;

namespace Weather.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherController : ControllerBase
	{
		private readonly ILogger<WeatherController> _logger;
		private readonly IOpenWeatherMapServices _openWeatherMapServices;

		public WeatherController(
			ILogger<WeatherController> logger,
			IOpenWeatherMapServices openWeatherMapServices)
		{
			_logger = logger;
			_openWeatherMapServices = openWeatherMapServices;
		}

		[HttpGet("coordinates", Name = "GetCoordinatesByLocationName")]
		public async Task<IActionResult> GetCoordinatesByLocationName([FromQuery] CoordinatesByLocationNameDto dto)
		{

			try
			{
				var result = await _openWeatherMapServices.GetCoordinatesByLocationName(dto);

				if (result == null)
				{
					_logger.LogInformation("No coordinates found for city: {City}", dto.LocationName);
					return NotFound($"No coordinates found for city: {dto.LocationName}");
				}

				_logger.LogInformation("Successfully retrieved coordinates for city: {City}", dto.LocationName);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while fetching coordinates for city: {City}", dto.LocationName);
				return StatusCode(500, "An internal server error occurred.");
			}

		}

		[HttpGet("weather", Name = "GetCurrentWeatherByCoordinates")]
		public async Task<IActionResult> GetCurrentWeatherByCoordinates([FromQuery] CurrentWeatherDto dto)
		{
			try
			{
				var result = await _openWeatherMapServices.CallCurrentWeatherData(dto);

				if (result == null)
				{
					_logger.LogInformation("No coordinates found for city: {City}", dto.LocationName);
					return NotFound($"No coordinates found for city: {dto.LocationName}");
				}

				_logger.LogInformation("Successfully retrieved coordinates for city: {City}", dto.LocationName);
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while fetching coordinates for city: {City}", dto.LocationName);
				return StatusCode(500, "An internal server error occurred.");
			}

		}

		//[HttpPost("locationbookmark", Name = "SaveLocationBookmark")]
		//public async Task<IActionResult> SaveLocationBookmark([FromBody] LocationBookmarkDto dto)
		//{

		//}

	}
}
