using AutoMapper;
using FlightFinderAPI.Contracts.Incoming;
using FlightFinderAPI.Domain;
using FlightFinderAPI.Repositories;
using FlightFinderAPI.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightFinderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
	private readonly AppDbContext _context;
	private readonly IFlightData _flightData;
	private readonly IMapper _mapper;

	public BookingController(AppDbContext context, IMapper mapper, IFlightData flightData)
	{
		_context = context;
		_mapper = mapper;
		_flightData = flightData;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Bookings == null) return NotFound();
		return await _context.Bookings.ToListAsync();
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Booking>> GetBooking(Guid id)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Bookings == null) return NotFound();
		var booking = await _context.Bookings.FindAsync(id);

		if (booking == null) return NotFound();

		return booking;
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutBooking(Guid id, Booking booking)
	{
		if (id != booking.BookingId) return BadRequest();

		_context.Entry(booking).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!BookingExists(id))
				return NotFound();
			throw;
		}

		return NoContent();
	}

	[HttpPost]
	public async Task<ActionResult<List<Booking>>> PostBooking(BookingCreationDto bookingRequest)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Bookings == null) return Problem("Entity set 'AppDbContext.Bookings'  is null.");

		var results = new List<Booking>();
		var bookingPossible = _flightData.BookFlightSeats(bookingRequest.RouteId, bookingRequest.FlightId,
			bookingRequest.NumberOfSeatsToBook);

		if (!bookingPossible) return StatusCode(406);
		for (var i = 0; i < bookingRequest.NumberOfSeatsToBook; i++)
		{
			var bookingResult = _mapper.Map<Booking>(bookingRequest);
			results.Add(bookingResult);

			_context.Bookings.Add(bookingResult);
			await _context.SaveChangesAsync();
		}

		return Ok(results);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteBooking(Guid id)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Bookings == null) return NotFound();
		var booking = await _context.Bookings.FindAsync(id);
		if (booking == null) return NotFound();

		_context.Bookings.Remove(booking);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private bool BookingExists(Guid id)
	{
		// ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
		return (_context.Bookings?.Any(e => e.BookingId == id)).GetValueOrDefault();
	}
}