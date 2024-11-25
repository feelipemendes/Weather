using Microsoft.Extensions.Options;
using System.Text.Json;
using Weather.Server.Dtos.OpenWeatherMap;
using Weather.Server.Services.Interfaces;
using Weather.Server.Settings;
using FluentValidation;
using FluentValidation.Results;

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

		public Task<CurrentWeatherResponseDto?> CallCurrentWeatherData(CurrentWeatherRequestDto weather)
		{
			throw new NotImplementedException();
		}

		public async Task<CoordinatesByLocationNameResponseDto?> GetCoordinatesByLocationName(CoordinatesByLocationNameRequestDto locationName)
		{
			var urlBase = _settings.UrlBase;
			var apiKey = _settings.ApiKey;

			var url = $"{urlBase}weather?q={locationName}&appid={apiKey}";

			try
			{
				var response = await _httpClient.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var result = JsonSerializer.Deserialize<CoordinatesByLocationNameResponseDto>(
						content,
						new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

					return result;
				}

				_logger.LogError("Erro ao chamar a API do OpenWeatherMap: {StatusCode} - {ReasonPhrase}",
				response.StatusCode, response.ReasonPhrase);

				return null;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao processar a chamada para a API do OpenWeatherMap.");
				return null;
			}
		}
	}
}
