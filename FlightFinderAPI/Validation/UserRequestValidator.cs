using FlightFinderAPI.Contracts.Incoming;
using FluentValidation;

namespace FlightFinderAPI.Validation;

public class UserRequestValidator : AbstractValidator<UserCreationDto>
{
	public UserRequestValidator()
	{
		RuleFor(opt => opt.UserEmail)
			.EmailAddress();
		RuleFor(opt => opt.Name)
			.MaximumLength(60);
		RuleFor(opt => opt.Password)
			.MinimumLength(10)
			.MaximumLength(60);
	}
}