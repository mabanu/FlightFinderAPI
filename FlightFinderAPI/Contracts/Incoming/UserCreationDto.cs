namespace FlightFinderAPI.Contracts.Incoming;

public class UserCreationDto
{
	public string Name { get; set; } = null!;
	public string UserEmail { get; set; } = null!;
	public string Password { get; set; } = null!;
}