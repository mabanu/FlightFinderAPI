namespace FlightFinderAPI.Contracts.Data.Incoming;

public class BookingCreationDto
{
	public string RouteId { get; set; }
	public string FlightId { get; set; }
	public string Currency { get; set; }
	public float Price { get; set; }
	public Guid UserId { get; set; }
}