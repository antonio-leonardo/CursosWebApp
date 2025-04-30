using Cursos.Domain.Domain.Models;

namespace Cursos.Domain.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetAll();
        Course Get(Guid courseId);
        Course Get(string courseTitle, string knowledgePlataformName, string instructorName);
        void Register(Course course);
        void Update(Course course);
        void Delete(Guid courseId);
    }
}