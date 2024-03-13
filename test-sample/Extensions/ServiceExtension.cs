using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using test_sample.Services;

namespace test_sample.Extensions 
{
  public static class ServiceExtensions
  {
    public static IServiceCollection AddTranslatorServices(this IServiceCollection services, IConfiguration config) 
    {
      services.AddTransient<MetricsDelegatingHandler>();
      services.AddHttpContextAccessor();
      services.AddHttpClient(TranslatorConstants.TRANSLATOR_CLIENT, client => {
        client.BaseAddress = new Uri(config["BaseUrl"]!, UriKind.Absolute);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      }).AddHttpMessageHandler<MetricsDelegatingHandler>();
      
      services.AddKeyedScoped<ITranslatorService, PirateTranslatorService>(TranslatorTypes.PIRATE);
      services.AddScoped<ITranslationDataService, TranslationDataService>();
      return services;
    }

    public static IServiceCollection AddUserAuthenticationServices(this IServiceCollection services) 
    {
        services.Configure<CookiePolicyOptions>(options => {
          options.CheckConsentNeeded = context => true;
          options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
          options.MinimumSameSitePolicy = SameSiteMode.Lax;
        });
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => {
          options.LoginPath  = "/Auth/Login";
          options.ReturnUrlParameter = "returnUrl";
        });
        return services;
    } 
  }
}