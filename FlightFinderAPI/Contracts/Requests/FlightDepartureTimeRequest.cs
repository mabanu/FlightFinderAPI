namespace FlightFinderAPI.Contracts.Requests;

public class FlightDepartureTimeRequest
{
	public DateTime DepartureTime { get; set; }
	public string DepartureDestination { get; set; } = null!;
}