using Cursos.Application.Helpers;
using Cursos.CrossCutting;
using Cursos.IoC.ServiceCollectionExtensions;
using Cursos.WebApp.Extensions;

namespace Cursos.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggingSingleton.InitializeLog();
            Logging log = LoggingSingleton.GetLogging();
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.ConfigureAppSettings();
            
            builder.Host.ConfigureServices((context, services) =>
            {
                var env = context.HostingEnvironment;
                services.RegisterServices(context.Configuration, builder.Environment, log).AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            });
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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