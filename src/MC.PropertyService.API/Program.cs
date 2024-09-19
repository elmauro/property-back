using MC.PropertyService.API.Data;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using MC.PropertyService.API.Validators;
using MC.PropertyService.API.Data.Repositories;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using MC.PropertyService.API.Options;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .WithExposedHeaders("Location");
        });
});

// Setting up logging with Serilog using settings from the app's configuration
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Configures HTTP logging to record the duration of HTTP requests
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.Duration;
});

builder.Services.AddControllers();

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "yourIssuer",   // Replace with your issuer
        ValidAudience = "yourAudience",   // Replace with your audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a-very-strong-256-bit-secret-key1234"))   // Replace with your secret key
    };
});

builder.Services.AddAuthorization();

// Adds caching services to store data in memory
builder.Services.AddMemoryCache();

// Configures the database context for PropertyDB using PostgreSQL
builder.Services.AddDbContext<PropertyDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgresdb")));

// Registers repository and service classes for dependency injection
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
builder.Services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();

// Registers AutoMapper to manage object-object mapping
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

// Sets up Fluent Validation for validating models
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<OwnerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyFilterValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyImageValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PropertyTraceValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PriceValidator>();

// Configures Swagger to help create interactive API documentation
ConfigureSwaggerOptions.AddSwagger(builder.Services);

// Adds MediatR for implementing mediator pattern in handling requests
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

// Middleware to enable HTTP logging
app.UseHttpLogging();

// If the app is not in production, enable Swagger UI
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Property API V1");
        c.RoutePrefix = "property";  // Serve Swagger UI under '/property'
    });

    var option = new RewriteOptions();
    option.AddRedirect("^$", "property/index.html"); // Redirect root URL to Swagger UI
    app.UseRewriter(option);
}

// Logs HTTP requests using Serilog
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

// Use CORS
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();

// Indicates the part of the program to exclude from code coverage measurement
[ExcludeFromCodeCoverage]
public partial class Program
{
}
