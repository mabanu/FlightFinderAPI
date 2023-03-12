using FlightFinderAPI.Contracts.Incoming;
using FlightFinderAPI.Repositories;
using FlightFinderAPI.Services.Context;
using FlightFinderAPI.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, service, configuration) => configuration
	.ReadFrom.Configuration(context.Configuration)
	.ReadFrom.Services(service)
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Month)
);

builder.Services.AddSingleton<IFlightData, FlightData>();
builder.Services.AddScoped<IValidator<UserCreationDto>, UserCreationDtoValidator>();
builder.Services.AddScoped<IValidator<BookingCreationDto>, BookingCreationDtoValidator>();
builder.Services.AddScoped<IValidator<UserUpdate>, UserUpdateValidator>();
builder.Services.AddScoped<IValidator<BookingUpdate>, BookingUpdateValidator>();
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