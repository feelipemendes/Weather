using Weather.Server.Dtos.OpenWeatherMap;

namespace Weather.Server.Services.Interfaces
{
	public interface IOpenWeatherMapServices
	{
		Task<List<CoordinatesByLocationNameResponseDto?>> GetCoordinatesByLocationName(CoordinatesByLocationNameDto coordinates);
		Task<CurrentWeatherResponseDto?> CallCurrentWeatherData(CurrentWeatherDto weather);
	}
}
