using System.Text.Json.Serialization;

namespace FlightFinderAPI.Domain;

public class Price
{
	[JsonPropertyName("currency")] public string Currency { get; set; } = null!;

	[JsonPropertyName("adult")] public decimal Adult { get; set; }

	[JsonPropertyName("child")] public decimal Child { get; set; }
}