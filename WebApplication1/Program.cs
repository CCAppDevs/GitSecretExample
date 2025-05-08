using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Data;
namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsDevelopment())
            {
                // development server connection
                builder.Services.AddDbContext<CustomerContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerContext") ?? throw new InvalidOperationException("Connection string 'CustomerContext' not found.")));

            }
            else if (builder.Environment.IsProduction())
            {
                builder.Services.AddDbContext<CustomerContext>(options =>
                    options.UseSqlServer(builder.Configuration["ConnectionStrings:CustomerContext"] ?? throw new InvalidOperationException("Connection string 'CustomerContext' not found.")));
            }
            else
            {
                throw new InvalidOperationException("No valid Environment to Create the Database");
            }


            // Add services to the container.

            builder.Services.AddControllers();

            var db = Environment.GetEnvironmentVariable("PATH");

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
