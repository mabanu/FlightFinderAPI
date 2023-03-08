using AutoMapper;
using FlightFinderAPI.Contracts.Data;
using FlightFinderAPI.Contracts.Data.Incoming;
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
	public async Task<ActionResult<User>> GetUser(Guid id)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Users == null) return NotFound();
		var user = await _context.Users.FindAsync(id);

		if (user == null) return NotFound();

		return user;
	}

	// PUT: api/User/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPut("{id}")]
	public async Task<IActionResult> PutUser(Guid id, User user)
	{
		if (id != user.UserId) return BadRequest();

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

	// POST: api/User
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<User>> PostUser(User user)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (_context.Users == null) return Problem("Entity set 'AppDbContext.Users'  is null.");
		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return CreatedAtAction("GetUser", new { id = user.UserId }, user);
	}

	// DELETE: api/User/5
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
		return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
	}
}