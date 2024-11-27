using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Weather.Server.Data;
using Weather.Server.Repositories;
using Weather.Server.Repositories.Interfaces;
using Weather.Server.Services;
using Weather.Server.Services.Interfaces;
using Weather.Server.Settings;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});


// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WeatherDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<OpenWeatherMapSettings>(
	builder.Configuration.GetSection("OpenWeatherMap"));

builder.Services.AddHttpClient<IOpenWeatherMapServices, OpenWeatherMapService>();

builder.Services.AddScoped<ILocationBookmarkRepository, LocationBookmarkRepository>();

var cultureInfo = new CultureInfo("en-US");
CultureInfo.CurrentCulture = cultureInfo;
CultureInfo.CurrentUICulture = cultureInfo;

var app = builder.Build();


// Aplicar a política "AllowAll"
app.UseCors("AllowAll");

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
