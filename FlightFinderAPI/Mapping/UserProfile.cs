using AutoMapper;
using FlightFinderAPI.Contracts.Incoming;
using FlightFinderAPI.Contracts.Responses;
using FlightFinderAPI.Domain;

namespace FlightFinderAPI.Mapping;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<User, UserDto>()
			.ForMember(dest => dest.Name,
				opt => opt.MapFrom(src => src.Name));

		CreateMap<UserCreationDto, User>()
			.ForMember(dest => dest.UserId,
				opt => opt.MapFrom(src => new Guid()));

		CreateMap<UserUpdate, User>();
	}
}