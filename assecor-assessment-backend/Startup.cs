using assecor_assessment_backend.Services.Color;
using assecor_assessment_backend.Services.Db;
using assecor_assessment_backend.Services.Logger;
using assecor_assessment_backend.Services.Path;
using assecor_assessment_backend.Services.Person;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace assecor_assessment_backend
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
            services.AddControllers();
            services.AddDbContext<DatabaseInteractor>(options => options.UseSqlite(Configuration.GetConnectionString("cs")));

            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<IPathService, PathService>();
            services.AddSingleton<IColorService, ColorService>();
            services.AddScoped<IPersonService, PersonService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
