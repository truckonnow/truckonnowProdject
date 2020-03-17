using ApiMobaileServise.BackgraundService;
using FluentScheduler;
using Microsoft.AspNetCore.Builder;         
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace WebDispacher
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 200; // 200 items max
                options.ValueLengthLimit = 1024 * 1024 * 500; // 100MB max len form data
            });
            System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            services.AddMvc();
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=RA}/{action=Index}/{id?}");
            });
        }
    }
}
