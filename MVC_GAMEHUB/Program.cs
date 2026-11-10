
using Microsoft.EntityFrameworkCore;
using MVC_GAMEHUB.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Permite que o React Native Web acesse a API
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactNativeWeb", policy =>
    {
        policy.WithOrigins("http://localhost:8081")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Conexão configurada fora do código
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException(
        "A string de conexão DefaultConnection não foi configurada.");
}

builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(connectionString));

var cultureInfo = new CultureInfo("pt-BR");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture =
        new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo),

    SupportedCultures = new[] { cultureInfo },
    SupportedUICultures = new[] { cultureInfo }
});

app.UseRouting();

app.UseCors("ReactNativeWeb");

app.UseSession();
app.UseAuthorization();

// Permite acessar Controllers com rotas da API,
// por exemplo: /api/jogos
app.MapControllers();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Logins}/{action=TelaDeLogin}/{id?}")
    .WithStaticAssets();

app.Run();