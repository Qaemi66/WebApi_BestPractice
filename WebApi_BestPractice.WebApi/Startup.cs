using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi_BestPractice.Data;
using WebApi_BestPractice.Data.Contracts;
using WebApi_BestPractice.Data.Repositories;
using WebFramework.Middlewares;
using FluentValidation.AspNetCore;
using WebApi_BestPractice.WebFramework.Extensions;
using WebApi_BestPractice.WebApi.Controllers;
using WebFramework.Filters;

namespace WebApi_BestPractice.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });

            services.
                AddMvc().
                AddCustomFluentValidation().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                
            services.AddRepositories();

            services.AddScoped<ApiResultFilterAttribute>();
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCustomExceptionHandler();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

}
