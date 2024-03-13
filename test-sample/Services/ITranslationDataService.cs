using test_sample.Data.Models;
using test_sample.Models;

namespace test_sample;

public interface ITranslationDataService
{
  Task<(List<Translation>, int total)> GetTranslations(DatableParam param);
}
