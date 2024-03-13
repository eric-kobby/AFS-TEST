using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test_sample.Models;
using test_sample.Services;

namespace test_sample.Controllers.Ap
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class TranslationsController : ControllerBase
  {
    private readonly ITranslatorService _translatorService;
    private readonly ITranslationDataService _translationDataService;

    public TranslationsController([FromKeyedServices(TranslatorTypes.PIRATE)]ITranslatorService translatorService, ITranslationDataService translationDataService)
    {
      _translatorService = translatorService;
      _translationDataService = translationDataService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTranslations([FromQuery]DatableParam param) 
    {
      var (translations,total) = await _translationDataService.GetTranslations(param);
      return Ok(new 
      {
        iTotalRecords = total,
        iTotalDisplayRecords = total,
        aaData = translations
      });
    }

    [HttpPost]
    public async Task<IActionResult> Translate([FromForm]TranslateDto dto)
    {
      var result = await _translatorService.Translate(dto.Text);
      return Ok(result);
    }
  }
}