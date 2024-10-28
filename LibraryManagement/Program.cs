
using LibraryManagementRepository.DbConfigure;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using LibraryManagement.Helper;

var builder = WebApplication.CreateBuilder(args);
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


var app = builder.Build();
 
// Seed roles and users at startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleInitializer.SeedRolesAndUsers(services);  
}
//

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
