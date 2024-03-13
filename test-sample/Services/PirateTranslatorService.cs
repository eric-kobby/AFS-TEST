using System.Text.Json;
using test_sample.Models;

namespace test_sample.Services {

    public class PirateTranslatorService : ITranslatorService
    {
      private readonly IHttpClientFactory _factory;
      public PirateTranslatorService(IHttpClientFactory factory)
      {
        _factory = factory;
      }
      public async Task<TranslatorResponse> Translate(string text)
      {
        using var client = _factory.CreateClient(TranslatorConstants.TRANSLATOR_CLIENT);
        var responseMessage = await client.PostAsJsonAsync(TranslatorTypes.PIRATE, new { text });
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TranslatorResponse>(response)!;
      }
    }
}