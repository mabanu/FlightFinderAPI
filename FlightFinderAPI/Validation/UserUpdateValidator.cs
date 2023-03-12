﻿using FlightFinderAPI.Contracts.Incoming;
using FluentValidation;

namespace FlightFinderAPI.Validation;

public class UserUpdateValidator : AbstractValidator<UserUpdate>
{
	public UserUpdateValidator()
	{
		RuleFor(opt => opt.UserId)
			.NotEmpty()
			.WithMessage("UserId can not be empty");

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