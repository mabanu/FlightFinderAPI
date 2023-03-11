namespace FlightFinderAPI.Contracts.Data.Incoming;

public class BookingCreationDto
{
	public string RouteId { get; set; } = null!;
	public string FlightId { get; set; } = null!;
	public string Currency { get; set; } = null!;

	// ReSharper disable once UnusedAutoPropertyAccessor.Global
	public float Price { get; set; }

	// ReSharper disable once UnusedAutoPropertyAccessor.Global
	public Guid UserId { get; set; }
	public int NumberOfSeatsToBook { get; set; }
}