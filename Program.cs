using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using myapp.Services;
using Swashbuckle.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your_issuer",
            ValidAudience = "your_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_very_long_and_secure_secret_key"))

        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context => {
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers(); // Add this line to register controllers

// Add authorization
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
// Always use Swagger/SwaggerUI for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Optional: Set the Swagger UI endpoint to the root
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at apps root
    });
}

app.UseAuthorization();

// Remove or comment out the original MapGet if they conflict or are not needed
// app.MapGet("/hello", () => "Hello World");
// var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";
// app.MapGet("/", () => $"Hello {target}!");

app.MapControllers(); // Add this line to map controller routes

app.Run(); // Remove the explicit URL parameter
