using AutoMapper;
using FlightFinderAPI.Contracts.Incoming;
using FlightFinderAPI.Domain;

namespace FlightFinderAPI.Mapping;

public class BookingProfile : Profile
{
	public BookingProfile()
	{
		CreateMap<BookingCreationDto, Booking>()
			.ForMember(dest => dest.BookingId,
				opt => opt.MapFrom(src => new Guid()))
			.ForMember(dest => dest.RouteId,
				opt => opt.MapFrom(src => src.RouteId))
			.ForMember(dest => dest.FlightId,
				opt => opt.MapFrom(src => src.FlightId))
			.ForMember(dest => dest.Currency,
				opt => opt.MapFrom(src => src.Currency))
			.ForMember(dest => dest.PriceToPay,
				opt => opt.MapFrom(src => src.Price))
			.ForMember(dest => dest.BookingDate,
				opt => opt.MapFrom(src => DateTime.UtcNow))
			.ForMember(dest => dest.UserId,
				opt => opt.MapFrom(src => src.UserId));
	}
}