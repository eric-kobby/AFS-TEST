using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace test_sample.DTO;

public class RegisterDto
{ 
  [FromForm(Name = "first_name")]
  public string FirstName { get; set; }  = default!;
  
  [FromForm(Name = "last_name")]
  public string LastName { get; set; }  = default!;
  public string Email { get; set; }  = default!;

  [JsonPropertyName("password")]
  public string Password { get; set; }  = default!;

  [FromForm(Name ="password_confirm")]
  public string PasswordConfirm { get; set; }  = default!;
}
