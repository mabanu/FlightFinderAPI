namespace FlightFinderAPI.Contracts.Requests;

public class BookFlightRequest
{
	public string RouteIdRequest { get; set; } = null!;
	public string FlightIdRequest { get; set; } = null!;
	public string Currency { get; set; } = null!;
	public float Price { get; set; }
	public Guid UserId { get; set; }
	public int NumberOfSeatsToBook => 0;
}