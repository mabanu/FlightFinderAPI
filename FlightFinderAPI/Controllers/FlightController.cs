using FlightFinderAPI.Contracts.Requests;
using FlightFinderAPI.Domain;
using FlightFinderAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlightFinderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
	private readonly IFlightData _flightData;

	public FlightController(IFlightData flightData)
	{
		_flightData = flightData;
	}

	[HttpGet("all")]
	public ActionResult<List<Flight>> Get()
	{
		return Ok(_flightData.GetFlights());
	}

	[HttpGet("route")]
	public ActionResult<List<Flight>> GetFlightBaseOnLocation([FromQuery] DepartureArrivalRequest request)
	{
		var result = _flightData.GetFlightsBaseOnLocation(request);

		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (result == null) return NotFound();

		return Ok(result);
	}

	[HttpGet("route-connection")]
	public ActionResult<List<Flight>> GetFlightConnection([FromQuery] DepartureArrivalRequest request)
	{
		var result = _flightData.GetFlightsConnection(request);

		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (result == null) return NotFound();

		return Ok(result);
	}

	[HttpGet("departures")]
	public ActionResult<List<Flight>> GetFlightsDeparture([FromQuery] string departure)
	{
		var result = _flightData.GetFlightsDeparture(departure);
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (result == null) return NotFound();

		return Ok(result);
	}

	[HttpGet("arrivals")]
	public ActionResult<List<Flight>> GetFlightsArrival([FromQuery] string arrival)
	{
		var result = _flightData.GetFlightsArrival(arrival);

		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (result == null) return NotFound();

		return Ok(result);
	}

	[HttpGet("time-routes")]
	public ActionResult<List<Flight>> GetFlightBaseOnTime([FromQuery] FlightTimeRequest request)
	{
		var result = _flightData.GetFlightsBaseOnTime(request);

		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (result == null) return NotFound();

		return Ok(result);
	}
}