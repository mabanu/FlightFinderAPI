using FlightFinderAPI.Contracts.Data.Incoming;
using FluentValidation;

namespace FlightFinderAPI.Validation;

public class UserRequestValidator : AbstractValidator<UserCreationDto>
{
	public UserRequestValidator()
	{
		RuleFor(opt => opt.UserEmail)
			.EmailAddress();
	}
}