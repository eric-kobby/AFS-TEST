using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace test_sample.Models;

#nullable disable
public class DatableParam
{
  private string search = string.Empty;
    private int startIndex = 0;

    [FromQuery(Name = "start")]
    public int StartIndex { get => startIndex; set => startIndex = value; }
    [FromQuery(Name = "length")]
  public int PageSize { get; set; } = 10;

  [FromQuery(Name = "search[value]")]
  public string Search { get => search; set => search = value ?? ""; }
  [FromQuery(Name = "order[0][dir]")]
  public string SortDirection { get; set; }

  [FromQuery(Name = "order[0][column]")]
  public int OrderColumn { get; set; }
}
