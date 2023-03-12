using FlightFinderAPI.Contracts.Incoming;
using FluentValidation;

namespace FlightFinderAPI.Validation;

public class UserCreationDtoValidator : AbstractValidator<UserCreationDto>
{
	public UserCreationDtoValidator()
	{
		RuleFor(opt => opt.UserEmail)
			.EmailAddress()
			.MaximumLength(60);
		RuleFor(opt => opt.Name)
			.NotEmpty()
			.MaximumLength(60)
			.WithMessage("Name can not be empty");
		RuleFor(opt => opt.Password)
			.MinimumLength(10)
			.MaximumLength(60);
	}
}