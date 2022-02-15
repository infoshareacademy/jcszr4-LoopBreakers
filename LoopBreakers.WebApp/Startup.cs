using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.DAL.Context;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.Services;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.DAL.Entities;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LoopBreakers.WebApp
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>().AddRoles<UserRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IRecipientService, RecipientService>();

            services.AddAutoMapper(typeof(Mappings.TransfersProfile));

            services.AddHttpContextAccessor();
            services.AddRazorPages();
            // services.AddIdentity<ApplicationUser, IdentityRole>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                var context = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context?.Database.Migrate();
                SeedData.SeedTransfer(context);
                SeedData.SeedClient(context);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }

    }
}
