using Cursos.Application.Interfaces;
using Cursos.Application.Services;
using Cursos.CrossCutting;
using Cursos.Domain.Interfaces.Repositories;
using Cursos.Infrastructure.Context;
using Cursos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace Cursos.IoC.ServiceCollectionExtensions
{
    public static class DependecyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env, Logging log)
        {
            log.Info("Injeção de dependência de Repositórios");
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IKnowledgePlatformRepository, KnowledgePlatformRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            log.Info("Injeção de dependência de Serviços");
            services.AddScoped<ICourseService, CourseService>();

            log.Info("Adição de contexto de Base de Dados");
            if (env.EnvironmentName != "Testing")
            {
                log.Info("Captura de connection string");
                var connectionString = config.GetConnectionString("CoursesDb");
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }
            else
            {
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("CoursesDb");
                });
                log.Info("Configuração do banco de dados InMemory para o ambiente de teste");
            }

            log.Info("Retorna todos os serviços configurados");
            return services;
        }
    }
}