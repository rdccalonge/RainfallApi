using AutoMapper;
using RainfallApi.Application.Services;
using RainfallApi.Core.Interfaces;
using RainfallApi.Infrastructure.Clients;

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
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IRainfallService, RainfallService>();

            
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
