using Swashbuckle.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Add this line to register controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

// Remove or comment out the original MapGet if they conflict or are not needed
// app.MapGet("/hello", () => "Hello World");
// var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";
// app.MapGet("/", () => $"Hello {target}!");

app.MapControllers(); // Add this line to map controller routes

app.Run(); // Remove the explicit URL parameter
