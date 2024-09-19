using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MC.PropertyService.API.Options
{
    /// <summary>
    /// Provides configuration for Swagger documentation.
    /// </summary>
    public static class ConfigureSwaggerOptions
    {
        /// <summary>
        /// Adds Swagger services to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to add the Swagger services to.</param>
        public static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Property {groupName}",
                    Version = groupName,
                    Description = "Property API",
                    Contact = new OpenApiContact
                    {
                        Name = "Personal Company",
                        Email = "elmauro@gmail.com",
                    }
                });

                // Configure JWT Bearer token authentication for Swagger
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below. Example: 'Bearer abc123xyz'"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}
