using Application;
using Infrastructure;
using Infrastructure.Communication;
using Microsoft.AspNetCore.Mvc;
using Presentation;
using Scalar.AspNetCore;
using Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region AppSettings
AppSettings.Init();
#endregion

#region Injecting Services (Injection Dependency)
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
#endregion


builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOnlyDefaults",
        policy =>
        {
            policy.WithOrigins(AppSettings.GetOriginRequest())
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});
#endregion

#region Presentation (JWT configuration, Scalar, etc)
builder.Services.AddPresentation();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    
}

app.UseMiddleware<ExceptionMiddleware>();

app.MapHub<NotificationHub>("/notificationHub");

app.UseHttpsRedirection();

app.UseCors("AllowOnlyDefaults");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
