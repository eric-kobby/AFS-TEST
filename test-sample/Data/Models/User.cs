using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace test_sample.Data.Models;

public class User
{
  [Key]
  [JsonIgnore]
  public int Id {get; set;}
  [JsonPropertyName("first_name")]
  public string FirstName { get; set; }  = default!;
  [JsonPropertyName("last_name")]
  public string LastName { get; set; }  = default!;
  public string Email { get; set; }  = default!;

  [JsonIgnore]
  public string Password { get; set; }  = default!;

}
