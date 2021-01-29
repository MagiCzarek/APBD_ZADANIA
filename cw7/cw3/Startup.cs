using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using cw3.Middleware;
using cw3.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using cw3.Models;

namespace cw3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>{
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "Server",
                    ValidAudience = "Employee",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        Configuration["SecretKey"]
                        ))
                };

            });
            services.AddScoped<IStudentDbService, SqlServerDbService>();
            services.AddTransient<IStudentDbService, SqlServerDbService>();
            services.AddControllers();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IStudentDbService dbService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<Logging>();

            app.Use(async (context, next) => {
                if (!context.Request.Headers.ContainsKey("Index"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Musisz podac numer indeksu");
                    return;
                }

                string index = context.Request.Headers["Index"].ToString();
                Console.WriteLine(index);
                Student student = dbService.GetStudent(index);
                if (student == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("Nie znaleziono studenta o indeksie " + index);
                    return;
                }
                context.Response.StatusCode = StatusCodes.Status200OK;
                await next();
            });

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
