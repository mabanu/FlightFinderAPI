using System.Text.Json.Serialization;

namespace FlightFinderAPI.Domain;

public class Price
{
	[JsonPropertyName("currency")] public string Currency { get; set; } = null!;

	[JsonPropertyName("adult")] public float Adult { get; set; }

	[JsonPropertyName("child")] public float Child { get; set; }
}