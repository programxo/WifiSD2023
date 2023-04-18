using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SD.Application.Extensions;
using SD.Application.Movies;
using SD.Persistence.Extensions;
using SD.Persistence.Repositories.DBContext;
using System.Reflection;
using MediatR;

namespace SD.WS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                /* Add services to the container. */

                builder.Services.AddControllers();

                /* DBContext */

                var connectionString = builder.Configuration.GetConnectionString("MovieDbContext");
                builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));

                /* Bootstrapping */

                builder.Services.RegisterRepositories();
                builder.Services.RegisterApplicationServices();

                /* MediatR */

                builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MovieQueryHandler).Assembly));
            }

            
            var app = builder.Build();
            {
                app.UseHttpsRedirection();
                app.MapControllers();

                app.Run();

            }

            // Configure the HTTP request pipeline.

            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            
        }
    }
}