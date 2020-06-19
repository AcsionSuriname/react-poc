using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIServer.Data;
using APIServer.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProofOfConcept
{
    public class Startup
    {
        private IWebHostEnvironment CurrentEnvironment { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            if (CurrentEnvironment.IsDevelopment())
                services.AddDbContext<APIServerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalConnectionString")));
            else
                services.AddDbContext<APIServerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AzureConnectionString")));

            services.AddCors();
            services.AddControllers();

            services.AddAuthentication(x => {
                                            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;})
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => options.ProofOfConceptJwtBearerOptions(Configuration));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                         .RequireAuthorization();
            });
        }
    }
}
