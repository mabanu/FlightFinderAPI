using System.Text.Json;
using FlightFinderAPI.Contracts.Requests;
using FlightFinderAPI.Domain;

namespace FlightFinderAPI.Repositories;

public class FlightData : IFlightData
{
	public FlightData()
	{
		List<Flight> flights;
		using (var reader = new StreamReader(@"Services/Data/data.json"))
		{
			var json = reader.ReadToEnd();
			flights = JsonSerializer.Deserialize<List<Flight>>(json)!;
		}

		Flights = flights;
	}

	private List<Flight> Flights { get; }

	public List<Flight> GetFlights()
	{
		return Flights;
	}

	public List<Flight> GetFlightsBaseOnLocation(DepartureArrivalRequest request)
	{
		var results = Flights
			.Where(flight => flight.DepartureDestination == request.DepartureRequest)
			.Where(flight => flight.ArrivalDestination == request.ArrivalRequest)
			.ToList();

		return results;
	}

	public List<Flight> GetFlightsDeparture(string departure)
	{
		var results = Flights
			.Where(flight => flight.DepartureDestination == departure)
			.ToList();

		return results;
	}

	public List<Flight> GetFlightsArrival(string arrival)
	{
		var results = Flights
			.Where(flight => flight.ArrivalDestination == arrival)
			.ToList();

		return results;
	}

	public List<Flight> GetFlightsBaseOnTime(FlightTimeRequest request)
	{
		var results = new List<Flight>();

		foreach (var flight in Flights)
		{
			var res = flight.Itineraries!.FindAll(itinerary => itinerary.DepartureAt == request.DepartureTime);
			flight.Itineraries = res;
			results.Add(flight);
		}

		return results;
	}

	public bool BookFlight(BookFlightRequest request)
	{
		var flight = Flights.Find(flight => flight.RouteId == request.RouteIdRequest);
		if (flight == null) return false;

		var itinerary = flight.Itineraries!.Find(itinerary => itinerary.FlightId == request.FlightIdRequest);
		if (itinerary == null) return false;

		if (itinerary.AvailableSeats <= request.NumberOfSeatsToBook) return false;

		itinerary.AvailableSeats -= request.NumberOfSeatsToBook;
		return true;
	}
}