using AutoMapper;
using FlightFinderAPI.Contracts.Responses;
using FlightFinderAPI.Domain;

namespace FlightFinderAPI.Mapping;

public class ItineraryProfile : Profile
{
	public ItineraryProfile()
	{
		CreateMap<Itinerary,ItineraryDirectFlightResponse>();
	}
}