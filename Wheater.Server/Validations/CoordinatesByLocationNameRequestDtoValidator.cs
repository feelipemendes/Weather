using FluentValidation;
using Weather.Server.Dtos.OpenWeatherMap;

namespace Weather.Server.Validations
{
	public class CoordinatesByLocationNameRequestDtoValidator : AbstractValidator<CoordinatesByLocationNameRequestDto>
	{
        public CoordinatesByLocationNameRequestDtoValidator()
		{
			RuleFor(x => x.LocationName)
				.NotEmpty().WithMessage("Location Name is required.")
				.MinimumLength(2).WithMessage("Location name minimum length is 2 characters.")
				.MaximumLength(100).WithMessage("Location name maximum length is 100 characters.");
		}
	}
}
