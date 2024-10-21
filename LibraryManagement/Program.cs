
using LibraryManagementRepository.DbConfigure;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using LibraryManagementModels.Entities;
using Microsoft.AspNetCore.Identity;
using LibraryManagementModels.BusinessModels;
using LibraryManagement.Helper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LibraryDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDbContextCS")));

builder.Services.AddProjectServices();

// Seq setup
// Step 1: Configure Serilog to use Seq
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Set log level
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)  // Adds environment name to logs
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Override Microsoft log level
    .Enrich.FromLogContext() // Enrich logs with contextual information
    .WriteTo.Console() // Write logs to console (optional)
    .WriteTo.Seq("http://localhost:5341") // Seq URL (default localhost)
    .WriteTo.File(
        path: "logs/myapp-log-.txt", // Path to the log file
        rollingInterval: RollingInterval.Day, // Create a new file every day
        retainedFileCountLimit: 7, // Retain log files for 7 days
        fileSizeLimitBytes: 10 * 1024 * 1024, // Max size of each log file (10MB)
        rollOnFileSizeLimit: true, // Create a new log file if size limit is reached
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}" // Customize log format
    ).CreateLogger();

// Step 2: Replace default logging with Serilog
builder.Host.UseSerilog();
//


// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<LibraryDbContext>()
    .AddDefaultTokenProviders();
//
// Configure cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
//

var app = builder.Build();

// role setup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleInitializer.SeedRoles(services); // Seed roles on application startup
}
//

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

app.Run();
