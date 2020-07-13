using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi_BestPractice.Data;
using WebFramework.Middlewares;
using WebApi_BestPractice.WebFramework.Extensions;
using WebFramework.Filters;
using Microsoft.AspNetCore.Mvc.Authorization;
using WebApi_BestPractice.Common;
using WebApi_BestPractice.Service.Services;
using WebFramework.Configuration;

namespace WebApi_BestPractice.WebApi
{
    public class Startup
    {
        //TODO:change default route in webapi

        private readonly SiteSettings _siteSettings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });

            services.AddCustomIdentity(_siteSettings.IdentitySettings);

            services.
                AddMvc(options=> {
                    options.Filters.Add(new AuthorizeFilter());
                }).
                AddCustomFluentValidation().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                
            services.AddRepositories();

            services.AddScoped<ApiResultFilterAttribute>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddJwtAuthentication(_siteSettings.jwtSettings);
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

            app.UseAuthentication();

            app.UseMvc();
        }
    }

}
