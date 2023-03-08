namespace FlightFinderAPI.Contracts.Data;

public class BookingDto
{
	public string RouterId { get; set; } = null!;
	public string FlightId { get; set; } = null!;
	public string Currency { get; set; } = null!;
	public float Price { get; set; }
}