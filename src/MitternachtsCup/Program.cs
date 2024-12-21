using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MitternachtsCup.Data;
using MitternachtsCup.Interfaces;
using MitternachtsCup.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ISpielRepository, SpielRepository>();
builder.Services.AddScoped<IGruppenRepository, GruppenRepository>();
builder.Services.AddScoped<IKoRepository, KoRepository>();
builder.Services.AddScoped<ITurnierplanRepository, TurnierplanRepository>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);         // Session Timeout konfigurieren
    options.Cookie.HttpOnly = true;                         // Session-Cookies sind HTTP-only
    options.Cookie.IsEssential = true;                      // Die Sitzung ist für die Anwendung essentiell
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    Seed.SeedData(app);
}

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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeeder.SeedAdminUser(services); // Seed-Admin-Benutzer erstellen
}

app.Run();



