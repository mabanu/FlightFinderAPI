using System.ComponentModel.DataAnnotations;

namespace FlightFinderAPI.Domain;

public class User
{
	[Key] public Guid UserId { get; set; }
	public string Name { get; set; } = null!;
	public string UserName { get; set; } = null!;
	public string Password { get; set; } = null!;
	public ICollection<Booking> BookedFlights { get; set; } = null!;
}