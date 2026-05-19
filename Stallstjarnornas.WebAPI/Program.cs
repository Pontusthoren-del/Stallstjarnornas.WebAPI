
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.Interfaces;
using Stallstjarnornas.WebAPI.Services;

namespace Stallstjarnornas.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<StallstjarnornasDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Stallstjarnornas.WebAPI")));

            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<ITableAssignmentService, TableAssignmentService>();
            builder.Services.AddScoped<IGuestService, GuestService>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
