using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi_BestPractice.Data.Contracts;
using WebApi_BestPractice.Data.Repositories;
using WebApi_BestPractice.Service.Services;
using WebApi_BestPractice.WebApi.Models;

namespace WebApi_BestPractice.WebFramework.Extensions
{
   public static class ServiceProviderExtension
    {
        public static IMvcBuilder AddCustomFluentValidation(this IMvcBuilder builder)
        {
            return builder.AddFluentValidation(fv =>
            {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;

                fv.RegisterValidatorsFromAssemblyContaining<UserDtoValidator>();
            });
        }
        
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClaimRepository, ClaimRepository>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
