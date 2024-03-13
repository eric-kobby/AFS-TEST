using Microsoft.EntityFrameworkCore;
using test_sample.Data.Contexts;
using test_sample.Data.Models;
using test_sample.DTO;
using test_sample.Models;

namespace test_sample.Services;

public class AuthService : IAuthService
{
  private readonly TranslationsDbContext _db;
  public AuthService(IDbContextFactory<TranslationsDbContext> dbContextFactory)
  {
    _db = dbContextFactory.CreateDbContext();
  }
  public async Task<(bool, User?)> Register(RegisterDto dto)
  {
    if (await _db.Users.AnyAsync(user => user.Email == dto.Email)) return (false, null);
    var user = new User
    {
      Email = dto.Email,
      Password = HashService.HashPassword(dto.Password),
      FirstName = dto.FirstName,
      LastName = dto.LastName
    };
    _db.Users.Add(user);
    await _db.SaveChangesAsync();
    return (true, user);
  }

  public async Task<LoginResult> Login(LoginDto dto)
  {
    var hashedPassword = HashService.HashPassword(dto.Password);
    var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == hashedPassword);
    if (user is null) return new(false);
    return new(true, user);
  }
}
