using System.ComponentModel.DataAnnotations;

namespace FlightFinderAPI.Domain;

public class User
{
	[Key] public Guid UserId { get; set; }

	[StringLength(60)] public string Name { get; set; } = null!;

	[StringLength(60)] public string UserEmail { get; set; } = null!;

	[StringLength(60)] public string Password { get; set; } = null!;

	public List<Booking> BookedFlights { get; set; } = null!;
}