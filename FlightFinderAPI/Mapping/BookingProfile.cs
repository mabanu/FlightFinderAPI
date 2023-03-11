using AutoMapper;
using FlightFinderAPI.Contracts.Incoming;
using FlightFinderAPI.Contracts.Responses;
using FlightFinderAPI.Domain;

namespace FlightFinderAPI.Mapping;

public class BookingProfile : Profile
{
	public BookingProfile()
	{
		CreateMap<BookingCreationDto, Booking>()
			.ForMember(dest => dest.BookingId,
				opt => opt.MapFrom(src => new Guid()))
			.ForMember(dest => dest.PriceToPay,
				opt => opt.MapFrom(src => src.Price))
			.ForMember(dest => dest.BookingDate,
				opt => opt.MapFrom(src => DateTime.UtcNow));

		CreateMap<Booking, BookingResponse>();

		CreateMap<BookingUpdate, Booking>();
	}
}