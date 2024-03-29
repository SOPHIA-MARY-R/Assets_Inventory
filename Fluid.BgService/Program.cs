using Blazored.LocalStorage;
using Fluid.BgService;
using Fluid.BgService.Authentication;
using Fluid.BgService.Extensions;
using Fluid.BgService.Models;
using Fluid.BgService.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Hosting.WindowsServices;
using MudBlazor.Services;

var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
};

var builder = WebApplication.CreateBuilder(options);

builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.ShowTransitionDuration = 100;
    config.SnackbarConfiguration.HideTransitionDuration = 100;
});
builder.Services.AddScoped(s => new HttpClient() { BaseAddress = new Uri(builder.Configuration.GetSection(nameof(MachineIdentifier)).GetValue<string>(nameof(MachineIdentifier.ServerAddress)))});
builder.Services.AddUserAuth();
builder.Services.AddSingleton<MachineIdentifierService>();
builder.Services.AddSingleton<TechnicianCredentialsService>();
builder.Services.AddSingleton<SystemConfigurationService>();
builder.Services.AddTransient<UserHttpClient>();
builder.Services.AddScoped<AuthenticationStateProvider, TechnicianAuthStateProvider>();
builder.Services.AddScoped<TechnicianAuthStateProvider>();
builder.Services.AddTransient<AuthorizationHeaderHandler>();

builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<BackgroundLoggerService>();

builder.Host.UseWindowsService();
builder.Host.UseSystemd();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();
