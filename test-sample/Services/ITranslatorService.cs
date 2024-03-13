using test_sample.Models;

namespace test_sample.Services {

  public interface ITranslatorService {
    Task<TranslatorResponse> Translate(string text);
  }
}