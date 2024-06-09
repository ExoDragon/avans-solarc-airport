using FlightManagementApi.DBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlightManagementApi.Repositories;
using FlightManagementApi.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FlightManagementApi
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
            services.AddDbContext<FlightManagementReadDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("FlightManagementReadCN")));

            services.AddDbContext<FlightManagementEventDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("FlightManagementEventCN")));

            services.AddHttpClient<WebserviceContext>(c =>
            {
                c.BaseAddress = new Uri(Configuration.GetConnectionString("ArrivalFlightsApi"));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                });

            services.AddAuthorization(options => options.AddPolicy("FlightEmployeeOnly", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c =>
                        (c.Type == "FlightEmployee" || c.Type == "Developer")
                    )
                )
            ));

            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IFlightEventRepository, FlightEventRepository>();
            services.AddScoped<IArrivalFlightRepository, ArrivalFlightRepository>();
            services.AddScoped<IFlightMapper, AviationStackFlightMapper>();

            RabbitMQMessageHandler messageHandler = new RabbitMQMessageHandler(services);
            services.AddSingleton<IMessageHandler, RabbitMQMessageHandler>(x => messageHandler);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightManagementApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FlightManagementReadDbContext readDbContext, FlightManagementEventDbContext eventDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightManagementApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            try
            {
                readDbContext.Database.Migrate();
            }
            catch { }

            try
            {
                eventDbContext.Database.Migrate();
            }
            catch { }
        }
    }
}
