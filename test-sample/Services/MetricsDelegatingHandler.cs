
using System.Text.Json;
using System.Threading.Channels;
using test_sample.Messages;
using test_sample.Models;

namespace test_sample.Services;
public class MetricsDelegatingHandler : DelegatingHandler
{
  
  private readonly ChannelWriter<TranslationMessage> _translationChannelWriter;
  public MetricsDelegatingHandler(Channel<TranslationMessage> channel)
  {
    _translationChannelWriter = channel.Writer;
  }
  protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
  {
    try
    {
      var responseMessage = await base.SendAsync(request, cancellationToken);
      responseMessage.EnsureSuccessStatusCode();
      var response = await responseMessage.Content.ReadAsStringAsync();
      var result = JsonSerializer.Deserialize<TranslatorResponse>(response)!;
      var contents = result.Contents;
      
      _translationChannelWriter.TryWrite(new TranslationMessage(contents.Text, contents.Translated, contents.Translation));
      return responseMessage;
    }
    catch (Exception)
    {
      throw;
    }
  }

}