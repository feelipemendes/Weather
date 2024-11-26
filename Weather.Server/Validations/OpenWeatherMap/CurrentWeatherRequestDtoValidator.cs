using FluentValidation;
using Weather.Server.Dtos.OpenWeatherMap;

namespace Weather.Server.Validations.OpenWeatherMap
{
	public class CurrentWeatherRequestDtoValidator : AbstractValidator<CurrentWeatherRequestDto>
	{
        public CurrentWeatherRequestDtoValidator()
        {
			RuleFor(x => x.lat)
			.InclusiveBetween(-90, 90)
			.WithMessage("Latitude must be between -90 and 90 degrees.");

			RuleFor(x => x.lon)
				.InclusiveBetween(-180, 180)
				.WithMessage("Longitude must be between -180 and 180 degrees.");

			RuleFor(x => x.LocationName)
				.NotEmpty()
				.WithMessage("Location name is required.")
				.MinimumLength(2)
				.WithMessage("Location name must be at least 2 characters long.")
				.MaximumLength(100)
				.WithMessage("Location name must not exceed 100 characters.");
		}
    }
}
