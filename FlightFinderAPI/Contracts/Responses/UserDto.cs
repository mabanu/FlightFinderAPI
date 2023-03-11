namespace FlightFinderAPI.Contracts.Responses;

public class UserDto
{
	public Guid UserId { get; set; }
	public string Name { get; set; } = null!;
}