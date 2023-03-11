using FlightFinderAPI.Contracts.Requests;
using FlightFinderAPI.Domain;

namespace FlightFinderAPI.Repositories;

public interface IFlightData
{
	List<Flight> GetFlights();
	bool BookFlightSeats(string routeId, string flightId, int numberOfSeats);
	List<Itinerary> GetFlightsBaseOnLocation(DepartureArrivalRequest request);
	List<Flight> GetFlightsConnection(DepartureArrivalRequest request);
	List<Flight> GetFlightsDeparture(string departure);
	List<Flight> GetFlightsArrival(string arrival);
	List<Itinerary> GetFlightsBaseOnDepartureTime(FlightDepartureTimeRequest request);
	List<Itinerary> GetFlightsBaseOnArrivalTime(FlightArrivalTimeRequest request);
}