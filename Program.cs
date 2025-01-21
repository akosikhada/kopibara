using Kopibara.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        // Configure Cookie authentication options (e.g., redirect to login page if unauthenticated)
        options.LoginPath = "/Auth/AdminLogin";
        options.LogoutPath = "/Home/Home";
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
        options.CallbackPath = "/signin-google";

        options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1500); // Adjust session timeout as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Essential for GDPR compliance
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use session, authentication, and authorization middleware in the correct order
app.UseSession();          // Add session before authentication and authorization
app.UseAuthentication();    // Authentication must come before authorization
app.UseAuthorization();

// Define the default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
