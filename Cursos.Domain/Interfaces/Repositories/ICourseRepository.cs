using Cursos.Domain.Domain.Models;

namespace Cursos.Domain.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetAll();
        Course Get(string courseTitle, string knowledgePlataformName, string instructorName);
        void Register(Course course);
    }
}