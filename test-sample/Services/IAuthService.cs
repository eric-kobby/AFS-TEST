using test_sample.Data.Models;
using test_sample.DTO;
using test_sample.Models;

namespace test_sample.Services;

public interface IAuthService
{
  Task<(bool, User?)> Register(RegisterDto dto);
  Task<LoginResult> Login(LoginDto dto);
}
