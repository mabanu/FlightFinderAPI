using FlightFinderAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace FlightFinderAPI.Services.Context;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users => Set<User>();
	public DbSet<Booking> Bookings => Set<Booking>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<User>()
			.HasData(new User
			{
				UserId = new Guid("BF182D5D-8489-48E6-8E87-DD2D90A7FBB6"),
				Name = "maxi",
				UserEmail = "maxi@maxi.com",
				Password = "maxi"
			});

		builder.Entity<Booking>()
			.HasData(new Booking
			{
				BookingId = new Guid("E910476F-3439-4728-A0D2-76BF92576740"),
				RouteId = "0e2f3647",
				FlightId = "6e4b483d",
				Currency = "SEK",
				PriceToPay = 140.39f,
				UserId = new Guid("BF182D5D-8489-48E6-8E87-DD2D90A7FBB6")
			});
	}
}