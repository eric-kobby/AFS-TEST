using test_sample.Data.Models;

namespace test_sample.Models;

public record LoginResult(bool Success, User? user = default);
