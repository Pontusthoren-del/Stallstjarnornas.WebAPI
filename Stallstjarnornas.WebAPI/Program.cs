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

            var useTestDb = builder.Configuration["UseTestDb"] == "true";
            var connectionString = useTestDb
                ? builder.Configuration.GetConnectionString("TestConnection")
                : builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<StallstjarnornasDbContext>(options =>
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly("Stallstjarnornas.WebAPI")));

            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<ITableAssignmentService, TableAssignmentService>();
            builder.Services.AddScoped<IGuestService, GuestService>();
            builder.Services.AddScoped<IMailLogService, MailLogService>();

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

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