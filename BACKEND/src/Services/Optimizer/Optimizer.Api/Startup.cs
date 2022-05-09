using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Optimizer.Api.Config;
using Optimizer.Services.BusinessLogic;
using Optimizer.Services.Proxies;
using Optimizer.Services.Proxies.Seeker;
using System.Reflection;

namespace Optimizer.Api
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
            services.AddProxiesRegistration(Configuration);

            services.AddControllers();

            services.Configure<ApiUrls>(
                opts => Configuration.GetSection("ApiUrls").Bind(opts)
                );
            services.AddHttpClient<ISeekerProxy, SeekerProxy>();
            services.AddHttpClient<IOptimizerLogic, OptimizerLogic>();

            services.AddMediatR(Assembly.Load("Optimizer.Services.SendtoSeeker"), Assembly.Load("Optimizer.Services.BusinessLogic"));

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
