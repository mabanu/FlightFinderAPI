namespace FlightFinderAPI.Contracts.Incoming;

public class BookingUpdate
{
	public Guid BookingId { get; set; }

	public string RouteId { get; set; } = null!;

	public string FlightId { get; set; } = null!;

	public string Currency { get; set; } = null!;

	public float PriceToPay { get; set; }

	public DateTime BookingDate { get; set; }

	public Guid UserId { get; set; }
}