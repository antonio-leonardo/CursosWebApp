using Cursos.Domain.Domain.Models;

namespace Cursos.Domain.Interfaces.Repositories
{
    public interface IInstructorRepository
    {
        Instructor Get(string completeName);
        void Register(Instructor instructor);
    }
}