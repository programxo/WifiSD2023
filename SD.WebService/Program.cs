using Microsoft.EntityFrameworkCore;
using SD.Application.Extensions;
using SD.Application.Movies;
using SD.Persistence.Extensions;
using SD.Persistence.Repositories.DBContext;
using Microsoft.AspNetCore.Authentication;
using SD.Application.Authentication;
using Wifi.SD.Core.Services;
using SD.Application.Services;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

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

                /* DB Context */

                var connectionString = builder.Configuration.GetConnectionString("MovieDbContext");
                builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));

                /* Bootstrapping */

                builder.Services.RegisterRepositories();
                builder.Services.RegisterApplicationServices();

                /* User Service Registration */

                builder.Services.AddScoped<IUserService, UserService>();

                /* MediatR */

                builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MovieQueryHandler).Assembly));

                /* Basic Authentication Handler Registration */

                builder.Services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

                builder.Services.AddSwaggerGen(g =>
                {
                    g.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Wifi SW-Developer 2022-2023", Version = "v1" });

                    /* Basic Authentication */

                    g.AddSecurityDefinition("basic", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Scheme = "basic",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Description = "Basic Authorization header using basic scheme"
                    });
                    g.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                        {
                            {

                            new OpenApiSecurityScheme()
                            {
                                Reference = new OpenApiReference()
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },

                            new string[] { }

                            }
                        });
                });


#if RELEASE
                /* Adding Https Redirection */

                builder.Services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                    options.HttpsPort = 44;
                });

                /* Configure Kestrel */

                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 80, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    });

                    options.Listen(IPAddress.Any, 443, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        listenOptions.UseHttps();
                    });
                });
#endif

                var app = builder.Build();
                {
                    app.UseHttpsRedirection();
                    app.UseAuthentication();
                    app.UseAuthorization();
                    app.MapControllers();



                    /* Configure the HTTP request pipeline. */

                    if (app.Environment.IsDevelopment())
                    {
                        app.UseSwagger();
                        app.UseSwaggerUI();
                    }


                    app.Run();
                }
            }
        }
    }
}