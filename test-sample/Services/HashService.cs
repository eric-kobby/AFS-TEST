using System.Security.Cryptography;
using System.Text;

namespace test_sample.Services;

public sealed class HashService
{ 
  public static string HashPassword(string password) 
  {
    using var sha256 = SHA256.Create();
    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
  }
}
