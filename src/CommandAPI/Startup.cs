using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandAPI.Controllers.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace CommandAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            /*
            var builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = Configuration.GetConnectionString("MSSqlConnection");
            builder.UserID = Configuration["UserID"];
            builder.Password = Configuration["Password"];
            services.AddDbContext<CommandContext>(opt =>
                opt.UseSqlServer(builder.ConnectionString));
                */

            services.AddDbContext<CommandContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("MSSqlConnection")));
            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
//            services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();
            services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
