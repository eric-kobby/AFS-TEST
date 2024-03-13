
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using test_sample.Data.Contexts;
using test_sample.Data.Models;
using test_sample.Messages;

namespace test_sample;

public class TranslationProcessingService : BackgroundService
{
  private readonly ChannelReader<TranslationMessage> _translationChannelReader;
  private readonly IDbContextFactory<TranslationsDbContext> _dbContextFactory;

  public TranslationProcessingService(Channel<TranslationMessage> channel, IDbContextFactory<TranslationsDbContext> dbContextFactory)
  {
    _translationChannelReader = channel.Reader;
    _dbContextFactory = dbContextFactory;
  }
  protected async override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var message in _translationChannelReader.ReadAllAsync(stoppingToken))
    {
      using var context = await _dbContextFactory.CreateDbContextAsync(stoppingToken);
      context.Translations.Add(new Translation
      {
        Text = message.Text,
        Translated = message.Translated,
        Translator = message.Translator
      });
      await context.SaveChangesAsync(stoppingToken);
    }
  }
}
