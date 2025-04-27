using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using myapp.Services;
using Swashbuckle.AspNetCore;
using myapp.Controllers;

var builder = WebApplication.CreateBuilder(args);

// ? Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ? JWT Auth
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
            OnChallenge = context => Task.CompletedTask
        };
    });

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<TokenService>();

builder.Services.AddTransient<EducationController>();
builder.Services.AddTransient<ExperienceController>();
builder.Services.AddTransient<ProjectsController>();
builder.Services.AddTransient<ServicesController>();
builder.Services.AddTransient<SkillsController>();



var app = builder.Build();

// ? Use CORS before everything else
app.UseCors("AllowAngularClient");

app.UseAuthentication();
app.UseAuthorization();

// ? Swagger (only for dev)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.Run();
