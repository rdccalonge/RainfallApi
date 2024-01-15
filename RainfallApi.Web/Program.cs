using AutoMapper;
using Microsoft.OpenApi.Models;
using RainfallApi.Application.Services;
using RainfallApi.Core.Interfaces;
using RainfallApi.Infrastructure.Clients;
using System.Reflection;

namespace RainfallApi.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var baseAddress = builder.Configuration.GetValue<string>("BaseUrl");

            builder.Services.AddScoped<IRainfallService, RainfallService>();
            builder.Services.AddScoped<IRainfallApiClient, RainfallApiClient>();

            // Add services to the container.
            builder.Services.AddHttpClient<IRainfallApiClient, RainfallApiClient>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Rainfall Api",
                    Version = "1.0",
                    Description = "An API which provides rainfall reading data",
                    Contact = new OpenApiContact
                    {
                        Name = "Sorted",
                        Url = new Uri("https://www.sorted.com")
                    }
                });

                c.AddServer(new OpenApiServer
                {
                    Url = "http://localhost:3000/",
                    Description = "Rainfall Api"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(filePath);

                c.DocumentFilter<TagDescriptionsDocumentFilter>();
            });

            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
