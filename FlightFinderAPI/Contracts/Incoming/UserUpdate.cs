namespace FlightFinderAPI.Contracts.Incoming;

public class UserUpdate
{
	public Guid UserId { get; set; }

	public string Name { get; set; } = null!;

	public string UserEmail { get; set; } = null!;

	public string Password { get; set; } = null!;
}