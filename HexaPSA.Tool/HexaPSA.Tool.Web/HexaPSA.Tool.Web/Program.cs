using FluentValidation;
using HexaPSA.Tool.Application.Behaviours;
using HexaPSA.Tool.Application.Extensions;
using HexaPSA.Tool.Infrastructure.Extensions;
using HexaPSA.Tool.Notifications;
using HexaPSA.Tool.Web.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            AddServices(builder);// Add services to the container.

            var app = builder.Build();
            ConfigureRequestPipeline(app); // Configure the HTTP request pipeline.

            app.Run();


        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

          var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name; //HexaPSA.Tool.Web

            var presentationAssembly = typeof(HexaPSA.Tool.API.AssemblyReference).Assembly;

            builder.Services.AddControllers()
                .AddApplicationPart(presentationAssembly);


            builder.Services.AddSwaggerGen(c =>
            {
                string fileName = presentationAssembly.GetName().Name;
                string presentationDocumentationFile = $"{fileName}.xml";

                string presentationDocumentationFilePath = Path.Combine(AppContext.BaseDirectory, presentationDocumentationFile);

                c.IncludeXmlComments(presentationDocumentationFilePath);

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
            });



            // Add cors
            builder.Services.AddCors();

            builder.Services.AddControllersWithViews();

           

            builder.Services.AddAutoMapper(typeof(Program));

            // Configurations
            builder.Services.Configure<AppSettings>(builder.Configuration);
           
            var applicationAssembly = typeof(HexaPSA.Tool.Application.AssemblyReference).Assembly;

         

            builder.Services.AddApplicationLayer();

            //Dapper
            builder.Services.AddInfrastructureLayer();

            builder.Services.AddTransient<ExceptionHandlingMiddleware>();


            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddValidatorsFromAssembly(applicationAssembly);

            //File Logger
            builder.Logging.AddFile(builder.Configuration.GetSection("Logging"));

            //Email
            builder.Services.Configure<MailConfiguration>(builder.Configuration.GetSection("SmtpConfig"));
            builder.Services.AddNotificationsLayer();

        }

        private static void ConfigureRequestPipeline(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
                app.UseSwagger();

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));

            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

          
           
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.Map("api/{**slug}", context =>
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            });

            app.MapFallbackToFile("index.html");
        }

         
    }
}