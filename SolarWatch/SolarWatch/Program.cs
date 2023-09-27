using SolarWatch.DatabaseServices.Repositories;
using SolarWatch.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IJsonProcessor, JsonProcessor>();
builder.Services.AddSingleton<ICityRepository, CityRepository>();
builder.Services.AddSingleton<ISunriseSunsetRepository, SunriseSunsetRepository>();
builder.Services.AddSingleton<IGeolocatorApi, GeolocatorApi>();
builder.Services.AddSingleton<ISaSApi, SaSApi>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();