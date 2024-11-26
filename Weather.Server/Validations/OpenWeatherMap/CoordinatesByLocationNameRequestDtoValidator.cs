using FluentValidation;
using Weather.Server.Dtos.OpenWeatherMap;

namespace Weather.Server.Validations.OpenWeatherMap
{
    public class CoordinatesByLocationNameRequestDtoValidator : AbstractValidator<CoordinatesByLocationNameDto>
    {
        public CoordinatesByLocationNameRequestDtoValidator()
        {
            RuleFor(x => x.LocationName)
                .NotEmpty().WithMessage("Location Name is required.")
                .MinimumLength(2).WithMessage("The location name must be at least 2 characters long.")
                .MaximumLength(100).WithMessage("The location name cannot exceed 100 characters.");
        }
    }
}
