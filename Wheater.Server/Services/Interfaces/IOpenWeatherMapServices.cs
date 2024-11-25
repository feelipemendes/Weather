using Weather.Server.Dtos.OpenWeatherMap;

namespace Weather.Server.Services.Interfaces
{
	public interface IOpenWeatherMapServices
	{
		Task<CoordinatesByLocationNameResponseDto?> GetCoordinatesByLocationName(CoordinatesByLocationNameRequestDto coordinates);
		Task<CurrentWeatherResponseDto?> CallCurrentWeatherData(CurrentWeatherRequestDto weather);
	}
}
