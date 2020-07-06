using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi_BestPractice.WebApi.Models;

namespace WebApi_BestPractice.WebFramework.Extensions
{
    public static class FluentValidationExtension
    {
        public static IMvcBuilder AddCustomFluentValidation(this IMvcBuilder builder) {
           return builder.AddFluentValidation(fv =>
            {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;

                fv.RegisterValidatorsFromAssemblyContaining<UserDtoValidator>();
            });
        }

    }
}
