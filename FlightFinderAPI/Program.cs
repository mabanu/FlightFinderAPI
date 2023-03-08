using FlightFinderAPI.Repositories;
using FlightFinderAPI.Services.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IFlightData, FlightData>();
builder.Services.AddCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
builder.Services.AddDbContext<AppDbContext>(opt =>
	opt.UseSqlite(builder.Configuration.GetValue<string>("Database:ConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(opt => opt
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();