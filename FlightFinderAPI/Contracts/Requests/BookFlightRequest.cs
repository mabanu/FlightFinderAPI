namespace FlightFinderAPI.Contracts.Requests;

public class BookFlightRequest
{
	public string RouteIdRequest { get; set; } = null!;
	public string FlightIdRequest { get; set; } = null!;
	public int NumberOfSeatsToBook => 0;
}