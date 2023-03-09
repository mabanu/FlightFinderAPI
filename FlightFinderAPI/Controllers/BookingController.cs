using FlightFinderAPI.Domain;
using FlightFinderAPI.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightFinderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
	private readonly AppDbContext _context;

	public BookingController(AppDbContext context)
	{
		_context = context;
	}

	// GET: api/Booking
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Bookings == null) return NotFound();
		return await _context.Bookings.ToListAsync();
	}

	// GET: api/Booking/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Booking>> GetBooking(Guid id)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Bookings == null) return NotFound();
		var booking = await _context.Bookings.FindAsync(id);

		if (booking == null) return NotFound();

		return booking;
	}

	// PUT: api/Booking/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

	// POST: api/Booking
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<Booking>> PostBooking(Booking booking)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Bookings == null) return Problem("Entity set 'AppDbContext.Bookings'  is null.");
		_context.Bookings.Add(booking);
		await _context.SaveChangesAsync();

		return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
	}

	// DELETE: api/Booking/5
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