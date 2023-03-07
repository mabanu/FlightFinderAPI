using FlightFinderAPI.Contracts.Requests;
using FlightFinderAPI.Domain;

namespace FlightFinderAPI.Repositories;

public interface IFlightData
{
	List<Flight> GetFlights();
	List<Flight> GetFlightsBaseOnLocation(DepartureArrivalRequest request);
	List<Flight> GetFlightsDeparture(string departure);
	List<Flight> GetFlightsArrival(string arrival);
	List<Flight> GetFlightsBaseOnTime(FlightTimeRequest request);
	bool BookFlight(BookFlightRequest request);
}