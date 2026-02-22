using D_A.Application.Services.Implementations;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Data;
using D_A.Infraestructure.Repository.Implementation;
using D_A.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Controllers y Views
builder.Services.AddControllersWithViews();

//Repositories
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();

//Services
builder.Services.AddScoped<IServiceUser, ServiceUser>();

//AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<UserProfile>();
});

//DbContext
var connectionString = builder.Configuration.GetConnectionString("SqlServerDataBase");
builder.Services.AddDbContext<DAContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
