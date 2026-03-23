using D_A.Application.Profiles;
using D_A.Application.Services.Implementations;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Data;
using D_A.Infraestructure.Repository.Implementation;
using D_A.Infraestructure.Repository.Interfaces;
using Libreria.Web.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

#region Logger
// Configuración Serilog
var logger = new LoggerConfiguration()
    // Nivel mínimo global (recomendado: Information)
    .MinimumLevel.Information()

    // Reducir ruido de logs internos de Microsoft
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    //Mostrar SQL ejecutado por EF Core
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information)

    // Enriquecer logs con contexto (RequestId, etc.)
    .Enrich.FromLogContext()

    // Consola: útil para depurar en Visual Studio
    .WriteTo.Console()

    // Archivos separados por nivel (rolling diario)
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
        .WriteTo.File(@"Logs\Info-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))

    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
        .WriteTo.File(@"Logs\Warning-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))

    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
        .WriteTo.File(@"Logs\Error-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))

    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
        .WriteTo.File(@"Logs\Fatal-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))

    .CreateLogger();

// Paso obligatorio ANTES de crear builder
Log.Logger = logger;

// Integrar Serilog al host
builder.Host.UseSerilog(Log.Logger);
#endregion 

//Controllers y Views
builder.Services.AddControllersWithViews();

//Repositories
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<IRepositoryAuctionBidHistory, RepositoryAuctionBidHistory>();
builder.Services.AddScoped<IRepositoryAuctions, RepositoryAuctions>();
builder.Services.AddScoped<IRepositoryObject, RepositoryObject>();
builder.Services.AddScoped<IRepositoryRole, RepositoryRole>();
builder.Services.AddScoped<IRepositoryCategory, RepositoryCategory>();
builder.Services.AddScoped<IRepositoryGender, RepositoryGender>();
builder.Services.AddScoped<IRepositoryQuaility, RepositoryQuality>();




//Services
builder.Services.AddScoped<IServiceUser, ServiceUser>();
builder.Services.AddScoped<IServiceAuctions, ServiceAuctions>();
builder.Services.AddScoped<IServiceAuctionBidHistory, ServiceAuctionBidHistory>();
builder.Services.AddScoped<IServiceObject, ServiceObject>();
builder.Services.AddScoped<IServiceCategories, ServiceCategories>();
builder.Services.AddScoped<IServiceQuality, ServiceQuality>();


builder.Services.AddScoped<IServiceCountry, ServiceCountry>();
builder.Services.AddScoped<IServiceGender, ServiceGender>();

//AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<UserProfile>();
    config.AddProfile<ObjectProfile>();
    config.AddProfile<AuctionProfile>();
    config.AddProfile<QualityProfile>();
    config.AddProfile<GenderProfile>();
    config.AddProfile<CountryProfile>();// <-- Añadido: registra el nuevo perfil de Genders
});

//DbContext
var connectionString = builder.Configuration.GetConnectionString("SqlServerDataBase");
builder.Services.AddDbContext<DAContext>(options =>
    options.UseSqlServer(connectionString));



 


//**** API ****
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];
builder.Services.AddHttpClient("DungeonsAndAuctionsApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
});
//**** API ****


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();


var app = builder.Build();






if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Middleware personalizado
    app.UseMiddleware<ErrorHandlingMiddleware>();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();


