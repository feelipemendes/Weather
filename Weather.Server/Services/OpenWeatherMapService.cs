using Microsoft.Extensions.Options;
using System.Text.Json;
using Weather.Server.Dtos.OpenWeatherMap;
using Weather.Server.Services.Interfaces;
using Weather.Server.Settings;
using Weather.Server.Validations.OpenWeatherMap;

namespace Weather.Server.Services
{
	public class OpenWeatherMapService : IOpenWeatherMapServices
	{
		private readonly OpenWeatherMapSettings _settings;
		private readonly HttpClient _httpClient;
		private readonly ILogger<OpenWeatherMapService> _logger;


		public OpenWeatherMapService(
			IOptions<OpenWeatherMapSettings> settings, 
			HttpClient httpClient,
			ILogger<OpenWeatherMapService> logger)
		{
			_settings = settings.Value;
			_httpClient = httpClient;
			_logger = logger;
		}

		public async Task<List<CoordinatesByLocationNameResponseDto?>> GetCoordinatesByLocationName(CoordinatesByLocationNameDto dto)
		{
			var validator = new CoordinatesByLocationNameRequestDtoValidator();
			var validation = validator.Validate(dto);

			
			if (validation.IsValid) 
			{
				var urlBase = _settings.UrlBaseGeocoding;
				var apiKey = _settings.ApiKey;

				var url = $"{urlBase}direct?q={dto.LocationName}&limit={5}&appid={apiKey}";

				try
				{
					var response = await _httpClient.GetAsync(url);

					if (response.IsSuccessStatusCode)
					{
						var content = await response.Content.ReadAsStringAsync();
						var result = JsonSerializer.Deserialize<List<CoordinatesByLocationNameResponseDto>>(
							content,
							new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

						return result;
					}

					_logger.LogError("Error calling OpenWeatherMap API: {StatusCode} - {ReasonPhrase}",
					response.StatusCode, response.ReasonPhrase);

					return null;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error processing call to OpenWeatherMap API.");
					return null;
				}
			}

			//todo: return errors
			return null;
		}

		public async Task<CurrentWeatherResponseDto?> CallCurrentWeatherData(CurrentWeatherDto weatherCoordinates)
		{
			var validator = new CurrentWeatherRequestDtoValidator();
			var validation = validator.Validate(weatherCoordinates);

			if (validation.IsValid)
			{
				var urlBase = _settings.UrlBase;
				var apiKey = _settings.ApiKey;

				var url = $"{urlBase}weather?lat={weatherCoordinates.lat}&lon={weatherCoordinates.lon}&appid={apiKey}";

				try
				{
					var response = await _httpClient.GetAsync(url);

					if (response.IsSuccessStatusCode)
					{
						var content = await response.Content.ReadAsStringAsync();
						var result = JsonSerializer.Deserialize<CurrentWeatherResponseDto>(
							content,
							new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

						return result;
					}

					_logger.LogError("Error calling OpenWeatherMap API: {StatusCode} - {ReasonPhrase}",
					response.StatusCode, response.ReasonPhrase);

					return null;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error processing call to OpenWeatherMap API.");
					return null;
				}

			}

			//todo: return errors
			return null;
		}
	}
}
