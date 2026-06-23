using Microsoft.EntityFrameworkCore;
using MVC_GAMEHUB.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Contexto>
    (options => options.UseSqlServer
    ("Data Source=TQR224232;Initial Catalog=CRUD_MVC_DO_MURILO;Integrated Security=False;User ID=tds;Password=tds123;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False"));

builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Logins}/{action=TelaDeLogin}/{id?}")
    .WithStaticAssets();

app.Run();