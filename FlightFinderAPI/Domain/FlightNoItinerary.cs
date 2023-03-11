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

	public FlightNoItinerary(FlightNoItinerary flight1, FlightNoItinerary flight2)
	{
		RouteId = flight1.RouteId + "," + flight2.RouteId;
		DepartureDestination = flight1.DepartureDestination;
		ArrivalDestination = flight2.ArrivalDestination;
	}

	public string RouteId { get; set; } = null!;

	public string DepartureDestination { get; set; } = null!;

	public string ArrivalDestination { get; set; } = null!;
}