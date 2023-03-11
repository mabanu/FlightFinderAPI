namespace FlightFinderAPI.Contracts.Requests;

public class FlightArrivalTimeRequest
{
	public DateTime ArrivalTime { get; set; }
	public string ArrivalDestination { get; set; }
}