
using LibraryManagementRepository.DbConfigure;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using LibraryManagement.Helper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<LibraryDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDbContextCS")));
var connectionString = builder.Configuration.GetConnectionString("LibraryDbContextCS");
builder.Services.ServiceCollectionsDI(connectionString);
 
//

 
// Step 1: Configure Serilog to use Seq
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() 
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)  
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) 
    .Enrich.FromLogContext()  
    .WriteTo.Console()  
    .WriteTo.Seq("http://localhost:5341") 
    .WriteTo.File(
        path: "logs/myapp-log-.txt",  
        rollingInterval: RollingInterval.Day,  
        retainedFileCountLimit: 7,  
        fileSizeLimitBytes: 10 * 1024 * 1024, 
        rollOnFileSizeLimit: true,  
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"  
    ).CreateLogger();

builder.Host.UseSerilog();
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
 
// Seed roles and users at startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleInitializer.SeedRolesAndUsers(services); // Seed roles and users on application startup
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
