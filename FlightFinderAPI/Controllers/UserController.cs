using AutoMapper;
using FlightFinderAPI.Contracts.Incoming;
using FlightFinderAPI.Contracts.Responses;
using FlightFinderAPI.Domain;
using FlightFinderAPI.Services.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightFinderAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;

	public UserController(AppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	// GET: api/User
	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Users == null) return NotFound();
		var allUsers = await _context.Users.ToListAsync();
		var users = _mapper.Map<IEnumerable<UserDto>>(allUsers);

		return Ok(users);
	}

	// GET: api/User/5
	[HttpGet("{id}")]
	public async Task<ActionResult<UserDto>> GetUser(Guid id)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Users == null) return NotFound();
		var user = await _context.Users.FindAsync(id);

		if (user == null) return NotFound();

		var result = _mapper.Map<UserDto>(user);

		return Ok(result);
	}

	[HttpGet("{id}/user-bookings")]
	public async Task<ActionResult<BookingResponse>> GetBookingOfUser(Guid id)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Bookings == null) return NotFound();

		var booking = await _context.Bookings
			.Where(booking => booking.UserId == id)
			.ToListAsync();

		var results = _mapper.Map<List<BookingResponse>>(booking);

		return Ok(results);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> PutUser(Guid id, UserUpdate userUpdate)
	{
		if (id != userUpdate.UserId) return BadRequest();

		var user = _mapper.Map<User>(userUpdate);

		_context.Entry(user).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!UserExists(id))
				return NotFound();
			throw;
		}

		return NoContent();
	}

	[HttpPost]
	public async Task<ActionResult<UserCreationDto>> PostUser([FromBody] UserCreationDto userCreation)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Users == null) return Problem("Entity set 'AppDbContext.Users'  is null.");
		var user = _mapper.Map<User>(userCreation);
		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return CreatedAtAction("GetUser", new { id = user.UserId }, userCreation);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteUser(Guid id)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Users == null) return NotFound();
		var user = await _context.Users.FindAsync(id);
		if (user == null) return NotFound();

		_context.Users.Remove(user);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private bool UserExists(Guid id)
	{
		// ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
		return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
	}
}