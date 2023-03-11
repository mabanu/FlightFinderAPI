namespace FlightFinderAPI.Domain;

public class FlightNoItinerary
{
	public FlightNoItinerary()
	{
	}

	public FlightNoItinerary(Flight flight)
	{
		RouteId = flight.RouteId;
		DepartureDestination = flight.DepartureDestination;
		ArrivalDestination = flight.ArrivalDestination;
	}

	public string RouteId { get; set; } = null!;

	public string DepartureDestination { get; set; } = null!;

	public string ArrivalDestination { get; set; } = null!;
}