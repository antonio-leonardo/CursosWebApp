using Cursos.Domain.Domain.Models;
using Cursos.Infrastructure.Context;

namespace Cursos.WebApp.Extensions
{
    public static class InitialData
    {
        public static void CreateInitialDevelopmentData(this DataContext context)
        {
            var platform = new KnowledgePlatform
            {
                Id = Guid.NewGuid(),
                Name = "Pluralsight",
                CreatedDate = DateTime.UtcNow
            };
            context.KnowledgePlatforms.Add(platform);

            var instructor = new Instructor
            {
                Id = Guid.NewGuid(),
                CompleteName = "Antonio Leonardo",
                CreatedDate = DateTime.UtcNow
            };
            context.Instructors.Add(instructor);
            context.SaveChanges();

            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = "Testes Integrados com C#",
                Description = "Testando ASP.NET Core Web APIs utilizando xUnit",
                Workload = 20,
                Notes = "Intro-level course",
                CreatedDate = DateTime.UtcNow,
                KnowledgePlatformId = platform.Id,
                InstructorId = instructor.Id,
                KnowledgePlatform = platform,
                Instructor = instructor
            };
            context.Courses.Add(course);
            context.SaveChanges();
        }
    }
}