using System.Text.Json;
using FlightFinderAPI.Contracts.Data.Incoming;
using FlightFinderAPI.Contracts.Requests;
using FlightFinderAPI.Domain;

namespace FlightFinderAPI.Repositories;

public class FlightData : IFlightData
{
	private readonly List<FlightNoItinerary> _flightNoItineraries;

	private readonly List<Flight> _flights;
	private readonly List<Itinerary> _itineraries;

	public FlightData()
	{
		List<Flight> flights;
		var itineraries = new List<Itinerary>();
		var flightNoItinerary = new List<FlightNoItinerary>();
		using (var reader = new StreamReader(@"Services/Data/data.json"))
		{
			var json = reader.ReadToEnd();
			flights = JsonSerializer.Deserialize<List<Flight>>(json)!;
		}

		foreach (var flight in flights)
		{
			var tempFlight = new FlightNoItinerary(flight);

			flightNoItinerary.Add(tempFlight);
			if (flight.Itineraries == null) continue;
			foreach (var itinerary in flight.Itineraries) itinerary.RouteId = flight.RouteId;
			itineraries.AddRange(flight.Itineraries);
		}

		_flights = flights;
		_flightNoItineraries = flightNoItinerary;
		_itineraries = itineraries;
	}

	public List<Flight> GetFlights()
	{
		var flights = new List<Flight>();
		foreach (var flight in _flightNoItineraries)
		{
			var allItinerary = _itineraries.FindAll(itinerary => itinerary.RouteId == flight.RouteId);
			var tempFlight = new Flight(flight, allItinerary);
			flights.Add(tempFlight);
		}

		return flights;
	}

	public bool BookFlightSeats(string routeId, string flightId, int numberOfSeats)
	{
		var result =
			_itineraries.FirstOrDefault(itinerary => itinerary.RouteId == routeId && itinerary.FlightId == flightId);

		if (result == null) return false;
		if (numberOfSeats >= result.AvailableSeats) return false;

		result.AvailableSeats -= numberOfSeats;
		return true;
	}

	public List<Itinerary> GetFlightsBaseOnLocation(DepartureArrivalRequest request)
	{
		var resultsFlight = _flightNoItineraries
			.FindAll(flight => flight.DepartureDestination == request.DepartureRequest &&
			                   flight.ArrivalDestination == request.ArrivalRequest);

		var results = new List<Itinerary>();

		foreach (var flight in resultsFlight)
		{
			var tempItinerary = _itineraries.FindAll(itinerary => itinerary.RouteId == flight.RouteId);

			results.AddRange(tempItinerary);
		}

		return results;
	}

	public List<Flight> GetFlightsConnection(DepartureArrivalRequest request)
	{
		var firstConnection = _flights
			.Where(flight => flight.DepartureDestination == request.DepartureRequest)
			.ToList();
		var secondConnection = _flights
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
		var resultsFlight = _flightNoItineraries
			.Where(flight => flight.DepartureDestination == departure)
			.ToList();

		var results = new List<Flight>();

		foreach (var flight in resultsFlight)
		{
			var itineraries = _itineraries.FindAll(itineraries => itineraries.RouteId == flight.RouteId);

			var tempFlight = new Flight(flight, itineraries);
			results.Add(tempFlight);
		}

		return results;
	}

	public List<Flight> GetFlightsArrival(string arrival)
	{
		var resultsFlight = _flightNoItineraries
			.Where(flight => flight.DepartureDestination == arrival)
			.ToList();

		var results = new List<Flight>();

		foreach (var flight in resultsFlight)
		{
			var itineraries = _itineraries.FindAll(itineraries => itineraries.RouteId == flight.RouteId);

			var tempFlight = new Flight(flight, itineraries);
			results.Add(tempFlight);
		}

		return results;
	}

	public List<Itinerary> GetFlightsBaseOnDepartureTime(FlightDepartureTimeRequest request)
	{
		var resultFlights =
			_flightNoItineraries.FindAll(flight => flight.DepartureDestination == request.DepartureDestination);

		var results = new List<Itinerary>();

		foreach (var flight in resultFlights)
		{
			var tempItinerary = _itineraries.FindAll(itinerary =>
				itinerary.RouteId == flight.RouteId && itinerary.DepartureAt == request.DepartureTime);

			results.AddRange(tempItinerary);
		}

		return results;
	}

	public List<Itinerary> GetFlightsBaseOnArrivalTime(FlightArrivalTimeRequest request)
	{
		var resultFlights =
			_flightNoItineraries.FindAll(flight => flight.DepartureDestination == request.ArrivalDestination);

		var results = new List<Itinerary>();

		foreach (var flight in resultFlights)
		{
			var tempItinerary = _itineraries.FindAll(itinerary =>
				itinerary.RouteId == flight.RouteId && itinerary.DepartureAt == request.ArrivalTime);

			results.AddRange(tempItinerary);
		}

		return results;
	}

	public bool BookFlight(BookingCreationDto request)
	{
		var flight = _flights.Find(flight => flight.RouteId == request.RouteId);
		if (flight == null) return false;

		var itinerary = flight.Itineraries!.Find(itinerary => itinerary.FlightId == request.FlightId);
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
						AwaitingTime = new DateTime(itinerary2.DepartureAt.Ticks - itinerary1.ArrivalAt.Ticks),
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