using Fluid.Core.Extensions;
using Fluid.Core.Interfaces;
using Fluid.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddDatabase(builder.Configuration, "DefaultConnection");
builder.Services.AddFeatures();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddTransient<DatabaseSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Services.CreateScope().ServiceProvider.GetService<DatabaseSeeder>()?.SeedAdminUser();

app.Run();
