using System.Security.Claims;
using System.Text;
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
                jwtBearerOptions.RequireHttpsMetadata = true;
                jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    NameClaimType = ClaimTypes.NameIdentifier,
                    RoleClaimType = ClaimTypes.Role,
                    //ValidIssuer = AppSettings.GetOriginIssuer(),
                    //ValidAudience = AppSettings.GetOriginAudience(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.GetAccessJwtSecret())),
                    ClockSkew = TimeSpan.Zero,
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
                        "Bienvenidos a la API - Clean Architecture + Hexagonal Architecture (version 1.0.0)";

                    return Task.CompletedTask;
                });
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });
            #endregion

            return services;
        }
    }
}