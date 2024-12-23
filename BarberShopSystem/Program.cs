using BarberShopSystem.Models;
using BarberShopSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Se añaden los servisler al contenedor.
builder.Services.AddScoped<rezervasyonlarService>();
builder.Services.AddScoped<ProfesyonellerService>();
builder.Services.AddScoped<ServislerService>();

// Se hace inyección de dependencia de la BD para SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "rezervasyonlar",
    pattern: "rezervasyonlar",
    defaults: new { controller = "Rezervasyonlar", action = "Index" }
);

app.MapControllerRoute(
    name: "profesyoneller",
    pattern: "profesyoneller",
    defaults: new { controller = "Profesyoneller", action = "Index" }
);

app.Run();
