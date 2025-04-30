using Cursos.Application.Helpers;
using Cursos.CrossCutting;
using Cursos.Infrastructure.Context;
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

            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (app.Environment.IsDevelopment())
            {
                IServiceScope scope = null;
                try
                {
                    log.Info("Cria escopo de Serviços");
                    scope = app.Services.CreateScope();

                    log.Info("Obtém o contexto de dados");
                    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    db.CreateInitialDevelopmentData();
                }
                catch (Exception ex)
                {
                    log.Error($"Surgiu um erro inesperado ao tentar inicializar o RPA, vide mensagem: {ex.Message}");
                }
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}