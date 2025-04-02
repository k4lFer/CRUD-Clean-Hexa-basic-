using System.Reflection;
using Application.Common.Behaviors;
using Application.Features.Order.Services;
using Application.Interfaces.Services;
using FluentValidation;
using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(typeof(AutoMapperProfile));
            
            #region Services    
            services.AddScoped<IOrderService, OrderService>();
            #endregion
            
            services.AddHttpContextAccessor();
            
            return services;
        }
    }
}
