using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightFinderAPI.Domain;

public class Booking
{
	[Key] public Guid BookingId { get; set; }

	[StringLength(60)] public string RouteId { get; set; } = null!;

	[StringLength(60)] public string FlightId { get; set; } = null!;

	[StringLength(8)] public string Currency { get; set; } = null!;

	public float PriceToPay { get; set; }

	public DateTime BookingDate { get; set; }

	[ForeignKey("User")] public Guid UserId { get; set; }
	public User User { get; set; } = null!;
}