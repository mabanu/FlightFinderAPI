using System.Text.Json;
using FlightFinderAPI.Contracts.Requests;
using FlightFinderAPI.Domain;

namespace FlightFinderAPI.Repositories;

public class FlightData : IFlightData
{
	private readonly List<FlightNoItinerary> _flightNoItineraries;

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

	public List<Flight> GetFlightsConnection(DepartureArrivalRequest request)
	{
		var flightsDeparture =
			_flightNoItineraries.FindAll(flight => flight.DepartureDestination == request.DepartureRequest);

		var flightsArrival = _flightNoItineraries.FindAll(flight => flight.ArrivalDestination == request.ArrivalRequest);

		var results = new List<Flight>();

		foreach (var flightDeparture in flightsDeparture)
		foreach (var flightArrival in flightsArrival)
			if (flightDeparture.ArrivalDestination == flightArrival.DepartureDestination)
			{
				var itineraryDeparture = _itineraries.FindAll(itinerary => itinerary.RouteId == flightDeparture.RouteId);
				var itineraryArrival = _itineraries.FindAll(itinerary => itinerary.RouteId == flightArrival.RouteId);
				var tempFlightNoItinerary = new FlightNoItinerary(flightDeparture, flightArrival);
				var tempItineraies = JoinItineraries(itineraryDeparture, itineraryArrival);
				var tempFlight = new Flight(tempFlightNoItinerary, tempItineraies);

				results.Add(tempFlight);
			}

		return results;
	}

	public List<Itinerary> JoinItineraries(List<Itinerary> itineraries1, List<Itinerary> itineraries2)
	{
		var result = new List<Itinerary>();

		foreach (var itinerary1 in itineraries1)
		foreach (var itinerary2 in itineraries2)
		{
			if (itinerary1.ArrivalAt > itinerary2.DepartureAt) continue;
			if (itinerary1.AvailableSeats < 1 || itinerary2.AvailableSeats < 1) continue;

			var tempItinerary = new Itinerary(itinerary1, itinerary2);

			result.Add(tempItinerary);
		}

		return result;
	}
}