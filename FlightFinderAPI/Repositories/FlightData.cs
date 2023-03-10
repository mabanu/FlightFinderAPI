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

	public List<Flight> GetFlightsConnection(DepartureArrivalRequest request)
	{
		var firstConnection = Flights
			.Where(flight => flight.DepartureDestination == request.DepartureRequest)
			.ToList();
		var secondConnection = Flights
			.Where(flight => flight.ArrivalDestination == request.ArrivalRequest)
			.ToList();

		var results = new List<Flight>();

		foreach (var flight1 in firstConnection)
		{
			var connectionFlight = secondConnection
				.FindAll(flight2 => flight2.DepartureDestination == flight1.ArrivalDestination);

			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (connectionFlight == null) continue;

			foreach (var flight2 in connectionFlight)
			{
				var joinFlight = CreateFlightFromConnection(flight1, flight2);

				results.Add(joinFlight);
			}
		}

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


	private Flight CreateFlightFromConnection(Flight flight1, Flight flight2)
	{
		var result = new Flight
		{
			RouteId = flight1.RouteId + "," + flight2.RouteId,
			DepartureDestination = flight1.DepartureDestination,
			ArrivalDestination = flight2.ArrivalDestination,
			Itineraries = new List<Itinerary>()
		};

		if (flight1.Itineraries != null)

			foreach (var itinerary1 in flight1.Itineraries)
			{
				if (flight2.Itineraries == null) continue;

				foreach (var itinerary2 in flight2.Itineraries
					         .Where(itinerary2 => itinerary1.ArrivalAt.AddHours(2) <= itinerary2.DepartureAt)
					         .Where(itinerary2 => Math.Min(itinerary1.AvailableSeats, itinerary2.AvailableSeats) >= 1))
					result.Itineraries.Add(new Itinerary
					{
						FlightId = itinerary1.FlightId + "," + itinerary2.FlightId,
						DepartureAt = itinerary1.DepartureAt,
						ArrivalAt = itinerary2.ArrivalAt,
						AvailableSeats = Math.Min(itinerary1.AvailableSeats, itinerary2.AvailableSeats),
						Prices = new Price
						{
							Currency = itinerary1.Prices.Currency,
							Adult = itinerary1.Prices.Adult + itinerary2.Prices.Adult,
							Child = itinerary1.Prices.Child + itinerary2.Prices.Child
						}
					});
			}

		return result;
	}
}