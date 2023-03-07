namespace FlightFinderAPI.Contracts.Requests;

public class DepartureArrivalRequest
{
	public string DepartureRequest { get; set; } = null!;
	public string ArrivalRequest { get; set; } = null!;
}