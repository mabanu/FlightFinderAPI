using FlightFinderAPI.Domain;

namespace FlightFinderAPI.Contracts.Responses;

public class ItineraryDirectFlightResponse
{
	public string FlightId { get; set; } = null!;

	public string RouteId { get; set; } = null!;

	public DateTime DepartureAt { get; set; }

	public DateTime ArrivalAt { get; set; }

	public int AvailableSeats { get; set; }

	public Price Prices { get; set; } = null!;
}