using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetcorePoc.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetcorePoc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add/register services to the container.
        // Both services, asp.netcore framework service like AddMvc and custom service like IMemberRepository
        // Interface, IServiceCollection is used to add our services to asp.net core dependency container
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // connects to the DB using SqlServer
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MemberDBConnection"))
                );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // Using AddScope method to have the instance of SqlMemberRepository class,
            // alive and available througout the entire scope of the http request
            // Injecting custom Dependency/service, IMemberRepository of Implementation, SqlMemberRepository
            services.AddScoped<IMemberRepository, SqlMemberRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
              //  app.UseHsts();
            }

            // middlewares
            // app.UseHttpsRedirection();
            // handles static files, imgae, css or js files and passes the mvc request to app.UseMvc
            app.UseStaticFiles();
            // app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
