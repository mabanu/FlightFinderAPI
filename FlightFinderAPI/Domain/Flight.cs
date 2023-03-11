using System.Text.Json.Serialization;

namespace FlightFinderAPI.Domain;

public class Flight
{
	public Flight()
	{
	}

	public Flight(FlightNoItinerary flightNoItinerary, List<Itinerary> itineraries)
	{
		RouteId = flightNoItinerary.RouteId;
		DepartureDestination = flightNoItinerary.DepartureDestination;
		ArrivalDestination = flightNoItinerary.ArrivalDestination;
		Itineraries = itineraries;
	}

	[JsonPropertyName("route_id")] public string RouteId { get; set; } = null!;

	[JsonPropertyName("departureDestination")]
	public string DepartureDestination { get; set; } = null!;

	[JsonPropertyName("arrivalDestination")]
	public string ArrivalDestination { get; set; } = null!;

	[JsonPropertyName("itineraries")] public List<Itinerary>? Itineraries { get; set; }
}