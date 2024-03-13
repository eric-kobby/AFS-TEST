using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace test_sample.Data.Models;

public class Translation
{
  [Key]
  [JsonIgnore]
  public int Id { get; set; }
  [Required]
  public string Text { get; set;} = default!;
  [Required]
  public string Translated { get; set; } = default!;
  [Required]
  public string Translator { get; set; } = default!;
}
