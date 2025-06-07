// Move the ConfigureServices method to a separate static class to resolve CS8803 and CS0106.  
// Also, ensure the method is used to resolve CS8321.  

using LesAmes.Application.Services;
using LesAmes.Domain.Authentication;
using LesAmes.Domain.Port;
using LesAmes.Infrastructure.Database;
using LesAmes.WebApi.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

// Add PostgreSQL  
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity  
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
   .AddEntityFrameworkStores<AppDbContext>()
   .AddDefaultTokenProviders();

// Add services to the container.  
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi  
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Configure JWT  
var key = Encoding.UTF8.GetBytes(Jwt.Secret); // Replace with a strong key  
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
       };
   });

builder.ConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();  

var summaries = new[]
{
   "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

await app.Services.MigrateDatabaseAsync();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Move ConfigureServices to a static class.  
public static class ServiceConfiguration
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserServiceIdentity>();
        builder.Services.AddScoped<UserService>();
    }
}