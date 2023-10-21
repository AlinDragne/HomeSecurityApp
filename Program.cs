using HomeSecurityApp.Areas.Identity;
using HomeSecurityApp.Data;
using HomeSecurityApp;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddHubOptions(o =>
    {
        o.MaximumReceiveMessageSize = 10 * 1024 * 1024; // Set to 10 MB in my use case
    });
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<FaceDetectionService>();
var framesFolderPath = "Data\\Frames";
builder.Services.AddSingleton<ImageService>(new ImageService(framesFolderPath));
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(IPAddress.Any, 5000);  // For HTTP
    serverOptions.Listen(IPAddress.Any, 44300);  // For HTTPS
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
   var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
   var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
   IdentityDataInitializer.SeedData(userManager, roleManager).GetAwaiter().GetResult();
}
app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
