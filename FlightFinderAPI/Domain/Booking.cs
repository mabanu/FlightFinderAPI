using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightFinderAPI.Domain;

public class Booking
{
	[Key] public Guid BookingId { get; set; }

	[StringLength(60)] [Required] public string RouteId { get; set; } = null!;

	[StringLength(60)] [Required] public string FlightId { get; set; } = null!;

	[StringLength(8)] [Required] public string Currency { get; set; } = null!;

	[Required] public float PriceToPay { get; set; }

	[Required] public DateTime BookingDate { get; set; }

	[ForeignKey("User")] public Guid UserId { get; set; }
	public User User { get; set; } = null!;
}