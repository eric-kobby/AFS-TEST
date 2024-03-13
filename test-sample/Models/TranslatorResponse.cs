using System.Text.Json.Serialization;

namespace test_sample.Models;

public class TranslatorResponse
{
  [JsonPropertyName("contents")]
  public TranslatorResponseContent Contents { get; set; } = default!; 
}

public class TranslatorResponseContent 
{
  [JsonPropertyName("translated")]
  public string Translated { get; set; } = default!;

  [JsonPropertyName("text")]
  public string Text { get; set; } = default!;
  [JsonPropertyName("translation")]
  public string Translation { get; set; } = default!;
}


