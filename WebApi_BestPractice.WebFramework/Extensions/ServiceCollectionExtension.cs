﻿using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi_BestPractice.Common;
using WebApi_BestPractice.Common.Enums;
using WebApi_BestPractice.Common.Exceptions;
using WebApi_BestPractice.Common.Utilities;
using WebApi_BestPractice.Data.Contracts;
using WebApi_BestPractice.Data.Repositories;
using WebApi_BestPractice.Service.Services;
using WebApi_BestPractice.WebApi.Models;

namespace WebApi_BestPractice.WebFramework.Extensions
{
   public static class ServiceCollectionExtension
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
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretkey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    RequireSignedTokens = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ValidateAudience = true, //default : false
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuer = true, //default : false
                    ValidIssuer = jwtSettings.Issuer
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationParameters;
                 options.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                         //logger.LogError("Authentication failed.", context.Exception);
                 
                         if (context.Exception != null)
                             throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);
                 
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = async context =>
                     {
                         //var applicationSignInManager = context.HttpContext.RequestServices.GetRequiredService<IApplicationSignInManager>();
                         var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                 
                         var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                         if (claimsIdentity.Claims?.Any() != true)
                             context.Fail("This token has no claims.");
                 
                         var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                         if (!securityStamp.HasValue())
                             context.Fail("This token has no secuirty stamp");
                 
                         //Find user and token from database and perform your custom validation
                         var userId = claimsIdentity.GetUserId<int>();
                         var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);
                 
                         if (user.SecurityStamp != Guid.Parse(securityStamp))
                             context.Fail("Token secuirty stamp is not valid.");
                 
                         //var validatedUser = await applicationSignInManager.ValidateSecurityStampAsync(context.Principal);
                         //if (validatedUser == null)
                         //    context.Fail("Token secuirty stamp is not valid.");
                 
                         if (!user.IsActive)
                             context.Fail("User is not active.");
                 
                         await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
                     },
                     OnChallenge = context =>
                     {
                         //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                         //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                 
                         if (context.AuthenticateFailure != null)
                             throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                         throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                 
                         //return Task.CompletedTask;
                     }
                 };
            });
        }
    }
}
