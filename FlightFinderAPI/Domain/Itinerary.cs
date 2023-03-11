using System.Text.Json.Serialization;

namespace FlightFinderAPI.Domain;

public class Itinerary
{
	public Itinerary()
	{
	}

	public Itinerary(Itinerary itinerary1, Itinerary itinerary2)
	{
		FlightId = itinerary1.FlightId + "," + itinerary2.FlightId;
		RouteId = itinerary1.RouteId + "," + itinerary2.RouteId;
		DepartureAt = itinerary1.DepartureAt;
		ArrivalAt = itinerary2.ArrivalAt;
		AwaitingTime = new TimeSpan(itinerary2.DepartureAt.Ticks - itinerary1.ArrivalAt.Ticks);
		Prices = new Price
		{
			Currency = itinerary1.Prices.Currency,
			Adult = itinerary1.Prices.Adult + itinerary2.Prices.Adult,
			Child = itinerary1.Prices.Child + itinerary2.Prices.Child
		};
		if (itinerary1.AvailableSeats > itinerary2.AvailableSeats) AvailableSeats = itinerary1.AvailableSeats;

		if (itinerary2.AvailableSeats > itinerary1.AvailableSeats) AvailableSeats = itinerary2.AvailableSeats;
	}

	[JsonPropertyName("flight_id")] public string FlightId { get; set; } = null!;

	public string RouteId { get; set; } = null!;

	[JsonPropertyName("departureAt")] public DateTime DepartureAt { get; set; }

	[JsonPropertyName("arrivalAt")] public DateTime ArrivalAt { get; set; }

	public TimeSpan AwaitingTime { get; set; }

	[JsonPropertyName("availableSeats")] public int AvailableSeats { get; set; }

	[JsonPropertyName("prices")] public Price Prices { get; set; } = null!;
}