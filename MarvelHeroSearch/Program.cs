using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MarvelHeroSearch.Data;
using MarvelHeroSearch.Client;
using MySql.Data.MySqlClient;
using System.Data;
using MarvelHeroSearch.Models.DbModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container //
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// DB Connection //
var connection = builder.Configuration.GetConnectionString("MarvelHeroes");
// During the scope of my request // 
builder.Services.AddScoped<IDbConnection>((s) =>
{
    IDbConnection conn = new MySqlConnection(connection);
    // Open before you can send a request to the DB //
    conn.Open();
    return conn;
});

//Add to your Program.cs Before .Build() to enable Hot Reload //
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Adding Dependency Injection for my DB //
builder.Services.AddTransient<IHeroRepository, HeroRepository>();

// Dependency Injection for the API //
builder.Services.AddTransient<IMarvelApiClient, MarvelApiClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

