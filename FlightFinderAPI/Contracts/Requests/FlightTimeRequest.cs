namespace FlightFinderAPI.Contracts.Requests;

public class FlightTimeRequest
{
	public DateTime DepartureTime { get; set; }
	public DateTime ArrivalTime { get; set; }
}