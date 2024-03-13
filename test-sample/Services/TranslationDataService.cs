using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using test_sample.Data.Contexts;
using test_sample.Data.Models;
using test_sample.Models;

namespace test_sample.Services;

public class TranslationDataService : ITranslationDataService
{
  private Dictionary<int, string> columnMap = new() 
  {
    { 0, "Text"},
    { 1, "Translated"},
    { 2, "Translator"},
  };
  private readonly IDbContextFactory<TranslationsDbContext> _dbContextFactory;

  public TranslationDataService(IDbContextFactory<TranslationsDbContext> dbContextFactory)
  {
    _dbContextFactory = dbContextFactory;
  }

  public async Task<(List<Translation>, int total)> GetTranslations(DatableParam param)
  {
    using var db = await _dbContextFactory.CreateDbContextAsync();
    var result = await db.Translations
     .FromSqlRaw("EXECUTE GET_TRANSLATIONS @SearchText, @SortColumn, @SortDirection, @StartIndex, @PageSize",
      new SqlParameter("@SearchText", param.Search),
      new SqlParameter("@SortColumn", columnMap[param.OrderColumn]),
      new SqlParameter("@SortDirection", param.SortDirection.ToUpper()),
      new SqlParameter("@StartIndex", param.StartIndex),
      new SqlParameter("@PageSize", param.PageSize))
     .ToListAsync();

     var total = param.Search == string.Empty ? await db.Translations.CountAsync() : result.Count;
    return (result, total);
  }
}
