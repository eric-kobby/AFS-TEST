using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using test_sample;
using test_sample.Data.Contexts;
using test_sample.Extensions;
using test_sample.Messages;
using test_sample.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContextFactory<TranslationsDbContext>(options => {
    options.UseSqlServer(configuration.GetConnectionString("Database"));
});
builder.Services.AddSingleton(Channel.CreateUnbounded<TranslationMessage>());
builder.Services.AddUserAuthenticationServices();
builder.Services.AddTranslatorServices(configuration);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddControllersWithViews();
builder.Services.AddHostedService<TranslationProcessingService>();
builder.Services.Configure<HostOptions>(hostOptions =>
{
    hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
