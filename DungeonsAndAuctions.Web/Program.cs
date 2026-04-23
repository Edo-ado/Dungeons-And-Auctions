

using D_A.Application.Config;
using D_A.Application.Profiles;
using D_A.Application.Services.Implementations;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Data;
using D_A.Infraestructure.Repository.Implementation;
using D_A.Infraestructure.Repository.Interfaces;
using Libreria.Web.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Text;
using DNDA.Web.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<AppConfig>(builder.Configuration);

#region Logger
var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
        .WriteTo.File(@"Logs\Info-.log", shared: true, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
        .WriteTo.File(@"Logs\Warning-.log", shared: true, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
        .WriteTo.File(@"Logs\Error-.log", shared: true, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
        .WriteTo.File(@"Logs\Fatal-.log", shared: true, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day))
    .CreateLogger();

Log.Logger = logger;
builder.Host.UseSerilog(Log.Logger);
#endregion


builder.Services.AddControllersWithViews(options =>
{
    
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.ResponseCacheAttribute
    {
        NoStore = true,
        Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.None
    });
});
builder.Services.AddSignalR();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";     
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Forbidden";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;               
    });


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".DNDA.Session";
});


builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<IRepositoryAuctionBidHistory, RepositoryAuctionBidHistory>();
builder.Services.AddScoped<IRepositoryAuctions, RepositoryAuctions>();
builder.Services.AddScoped<IRepositoryObject, RepositoryObject>();
builder.Services.AddScoped<IRepositoryRole, RepositoryRole>();
builder.Services.AddScoped<IRepositoryCategory, RepositoryCategory>();
builder.Services.AddScoped<IRepositoryGender, RepositoryGender>();
builder.Services.AddScoped<IRepositoryQuaility, RepositoryQuality>();
builder.Services.AddScoped<IRepositoryAuctionWinner, RepositoryAuctionWinner>();
builder.Services.AddScoped<IRepositoryPayment, RepositoryPayment>();


builder.Services.AddScoped<IServiceUser, ServiceUser>();
builder.Services.AddScoped<IServiceAuctions, ServiceAuctions>();
builder.Services.AddScoped<IServiceAuctionBidHistory, ServiceAuctionBidHistory>();
builder.Services.AddScoped<IServiceObject, ServiceObject>();
builder.Services.AddScoped<IServiceCategories, ServiceCategories>();
builder.Services.AddScoped<IServiceQuality, ServiceQuality>();
builder.Services.AddScoped<IServicePayment, ServicePayment>();
builder.Services.AddScoped<IServiceAuctionWinner, ServiceAuctionWinner>();
builder.Services.AddScoped<IServiceCountry, ServiceCountry>();
builder.Services.AddScoped<IServiceGender, ServiceGender>();


builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<UserProfile>();
    config.AddProfile<ObjectProfile>();
    config.AddProfile<AuctionProfile>();
    config.AddProfile<AuctionBidHistoryProfile>();
    config.AddProfile<QualityProfile>();
    config.AddProfile<GenderProfile>();
    config.AddProfile<CountryProfile>();
});


var connectionString = builder.Configuration.GetConnectionString("SqlServerDataBase");
builder.Services.AddDbContext<DAContext>(options =>
    options.UseSqlServer(connectionString));


var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];
builder.Services.AddHttpClient("DungeonsAndAuctionsApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseSession();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapHub<AuctionHub>("/auctionHub");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();