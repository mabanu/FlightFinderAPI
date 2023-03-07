using System.Text.Json.Serialization;

namespace FlightFinderAPI.Domain;

public class Itinerary
{
	[JsonPropertyName("flight_id")] public string FlightId { get; set; } = null!;

	[JsonPropertyName("departureAt")] public DateTime DepartureAt { get; set; }

	[JsonPropertyName("arrivalAt")] public DateTime ArrivalAt { get; set; }

	[JsonPropertyName("availableSeats")] public int AvailableSeats { get; set; }

	[JsonPropertyName("prices")] public Price Prices { get; set; } = null!;
}