using Cursos.CrossCutting;
using Cursos.Domain.Domain.Models;
using Cursos.Domain.Interfaces.Repositories;
using Cursos.Infrastructure.Context;

namespace Cursos.Infrastructure.Repositories
{
    public class InstructorRepository : LoggingBase, IInstructorRepository
    {
        private readonly DataContext _context;
        public InstructorRepository(DataContext context)
        {
            _context = context;
        }
        public void Register(Instructor instructor)
        {
            Log.Info($"Persistencia de professor, Id: {instructor.Id}");
            _context.Instructors.Add(instructor);
        }

        public Instructor Get(string completeName)
        {
            Log.Info($"Obtenção de professor pelo nome completo \"{completeName}\"");
            return _context.Instructors.Where(p => p.CompleteName.Replace(" ", "").ToLower() == completeName.Replace(" ", "").ToLower()).FirstOrDefault();
        }
    }
}