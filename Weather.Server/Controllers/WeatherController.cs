using Microsoft.AspNetCore.Mvc;
using Weather.Server.Dtos;
using Weather.Server.Dtos.OpenWeatherMap;
using Weather.Server.Entities;
using Weather.Server.Repositories.Interfaces;
using Weather.Server.Services.Interfaces;

namespace Weather.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherController : ControllerBase
	{
		private readonly ILogger<WeatherController> _logger;
		private readonly IOpenWeatherMapServices _openWeatherMapServices;
		private readonly ILocationBookmarkRepository _locationBookmarkRepository;

		public WeatherController(
			ILogger<WeatherController> logger,
			IOpenWeatherMapServices openWeatherMapServices,
			ILocationBookmarkRepository locationBookmarkRepository)
		{
			_logger = logger;
			_openWeatherMapServices = openWeatherMapServices;
			_locationBookmarkRepository = locationBookmarkRepository;
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

		[HttpPost("locationbookmark", Name = "SaveLocationBookmark")]
		public async Task<IActionResult> SaveLocationBookmark([FromBody] LocationBookmarkDto dto)
		{
			try
			{
				var result = await _locationBookmarkRepository.SaveLocationBookmarkAsync(dto);
				if (result)
				{
					_logger.LogInformation("Successfully saved location bookmark for {LocationName}.", dto.LocationName);
					return Ok("Location bookmark saved successfully.");
				}

				_logger.LogWarning("Failed to save location bookmark for {CiLocationNamety}.", dto.LocationName);
				return BadRequest("Failed to save location bookmark.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while saving the location bookmark for {LocationName}.", dto.LocationName);
				return StatusCode(500, "An internal server error occurred.");
			}
		}

		[HttpGet("locationbookmarks", Name = "GetAllBookmarkedLocations")]
		public async Task<IActionResult> GetAllBookmarkedLocations()
		{
			try
			{
				var bookmarks = await _locationBookmarkRepository.GetAllBookmarkedLocationsAsync();
				if (bookmarks == null || !bookmarks.Any())
				{
					_logger.LogInformation("No location bookmarks found.");
					return NotFound("No location bookmarks found.");
				}

				_logger.LogInformation("Successfully retrieved all location bookmarks.");
				return Ok(bookmarks);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving location bookmarks.");
				return StatusCode(500, "An internal server error occurred.");
			}
		}

	}
}
