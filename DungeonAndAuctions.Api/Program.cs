using D_A.Application.Profiles;
using D_A.Application.Profiles.Api;
using D_A.Application.Services.Implementations.Api;
using D_A.Application.Services.Interfaces.Api;
using D_A.Infraestructure.Data;
using D_A.Infraestructure.Repository.Implementation;
using D_A.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


var connectionString = builder.Configuration.GetConnectionString("SqlServerDataBase");
builder.Services.AddDbContext<DAContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();


builder.Services.AddScoped<IUserApiQueryService, ServiceUserApiQueryService>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ApiProfile>();
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.Redirect("/swagger"));

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();