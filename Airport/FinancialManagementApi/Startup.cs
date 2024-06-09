using FinancialManagementApi.DBContext;
using FinancialManagementApi.Handlers;
using FinancialManagementApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementApi
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
            services.AddDbContext<FinancialManagementReadDbContext>(options => options.UseSqlServer(
            Configuration.GetConnectionString("FinancialManagementReadCN")));

            services.AddDbContext<FinancialManagementEventDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("FinancialManagementEventCN")));

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

            services.AddAuthorization(options => options.AddPolicy("FinancialEmployeeOnly", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c =>
                        (c.Type == "FinancialEmployee" || c.Type == "Developer")
                    )
                )
            ));

            services.AddScoped<IFinancialEventRepository, FinancialEventRepository>();
            services.AddScoped<IFinancialRepository, FinancialRepository>();

            RabbitMQMessageHandler messageHandler = new RabbitMQMessageHandler(services);
            services.AddSingleton<IMessageHandler, RabbitMQMessageHandler>(x => messageHandler);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinancialManagementApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FinancialManagementEventDbContext eventDbContext, FinancialManagementReadDbContext readDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinancialManagementApi v1"));
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
                eventDbContext.Database.Migrate();
            }
            catch { }

            try
            {
                readDbContext.Database.Migrate();
            }
            catch { }
        }
    }
}
