using System.Text.Json.Serialization;

namespace FlightFinderAPI.Domain;

public class Flight
{
	[JsonPropertyName("route_id")] public string RouteId { get; set; } = null!;

	[JsonPropertyName("departureDestination")]
	public string DepartureDestination { get; set; } = null!;

	[JsonPropertyName("arrivalDestination")]
	public string ArrivalDestination { get; set; } = null!;

	[JsonPropertyName("itineraries")] public List<Itinerary>? Itineraries { get; set; }
}