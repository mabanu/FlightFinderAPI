using FlightFinderAPI.Contracts.Incoming;
using FluentValidation;

namespace FlightFinderAPI.Validation;

public class BookingCreationDtoValidator : AbstractValidator<BookingCreationDto>
{
	public BookingCreationDtoValidator()
	{
		RuleFor(prop => prop.RouteId)
			.NotEmpty()
			.MaximumLength(60)
			.WithMessage("RouteId can not be empty");

		RuleFor(prop => prop.FlightId)
			.NotEmpty()
			.MaximumLength(60)
			.WithMessage("FlightId can not be empty");

		RuleFor(prop => prop.Currency)
			.NotEmpty()
			.MaximumLength(8)
			.WithMessage("Currency can not be empty");

		RuleFor(prop => prop.Price)
			.NotEmpty()
			.WithMessage("Price can not be empty");

		RuleFor(prop => prop.UserId)
			.NotEmpty()
			.WithMessage("FlightId can not be empty");
	}
}