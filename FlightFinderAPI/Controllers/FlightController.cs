using AutoMapper;
using FlightFinderAPI.Contracts.Requests;
using FlightFinderAPI.Contracts.Responses;
using FlightFinderAPI.Domain;
using FlightFinderAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlightFinderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
	private readonly IFlightData _flightData;
	private readonly IMapper _mapper;

	public FlightController(IFlightData flightData, IMapper mapper)
	{
		_flightData = flightData;
		_mapper = mapper;
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

	[HttpGet("departure-time-routes")]
	public ActionResult<List<ItineraryDirectFlightResponse>> GetFlightBaseOnDepartureTime(
		[FromQuery] FlightDepartureTimeRequest request)
	{
		var itineraries = _flightData.GetFlightsBaseOnDepartureTime(request);

		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (itineraries == null) return NotFound();

		var results = _mapper.Map<List<ItineraryDirectFlightResponse>>(itineraries);

		return Ok(results);
	}

	[HttpGet("arrival-time-routes")]
	public ActionResult<List<ItineraryDirectFlightResponse>> GetFlightBaseOnArrivalTime(
		[FromQuery] FlightArrivalTimeRequest request)
	{
		var itineraries = _flightData.GetFlightsBaseOnArrivalTime(request);

		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (itineraries == null) return NotFound();

		var results = _mapper.Map<List<ItineraryDirectFlightResponse>>(itineraries);

		return Ok(results);
	}
}