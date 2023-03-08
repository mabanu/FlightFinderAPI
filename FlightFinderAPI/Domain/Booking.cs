using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightFinderAPI.Domain;

public class Booking
{
	[Key] public Guid BookingId { get; set; }
	public string RouteId { get; set; } = null!;
	public string FlightId { get; set; } = null!;
	public string Currency { get; set; } = null!;
	public float PriceToPay { get; set; }
	[ForeignKey("User")] public Guid UserId { get; set; }
	public User User { get; set; } = null!;
}