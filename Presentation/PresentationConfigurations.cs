using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.Scalar;
using Shared.Settings;

namespace Presentation
{
    public static class PresentationConfigurations
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            #region JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.SaveToken = true;
                //jwtBearerOptions.RequireHttpsMetadata = true;
                jwtBearerOptions.RequireHttpsMetadata = false; // Solo para desarrollo
                jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    NameClaimType = ClaimTypes.NameIdentifier,
                    RoleClaimType = ClaimTypes.Role,
                    ValidIssuer = AppSettings.GetIssuer(),
                    ValidAudience = AppSettings.GetAudience(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.GetAccessJwtSecret())),
                    ClockSkew = TimeSpan.Zero,
                };
                // Eventos
                jwtBearerOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"message\": \"Unauthorized. Token no valido o expirado.\"}");
                    }
                };
            });
            services.AddAuthorization();
            #endregion

            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            #region OpenAPI with Scalar
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Info.Version = "1.0.0";
                    document.Info.Title = "Api";
                    document.Info.Description =
                        "Bienvenidos a la API - (version 1.0.0)"
                        + "\n\n" + 
                        "Esta API es una prueba de arquitecturas: Hexagonal, Clean Architecture con DDD."
                        + "\n\n" +
                        "Desarrollada por un estudiante de informatica y sistemas, con el objetivo de aprender y aplicar conceptos de arquitectura de software." ;
                    

                    return Task.CompletedTask;
                });
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });
            #endregion

            return services;
        }
    }
}