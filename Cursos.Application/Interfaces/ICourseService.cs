using Cursos.Application.Dto;

namespace Cursos.Application.Interfaces
{
    public interface ICourseService
    {
        CourseDto GetById(Guid courseId);
        List<CourseDto> GetAll();
        Guid Register(CourseDto courseDto);
        void Update(Guid courseId, CourseDto courseDto);
        void Delete(Guid courseId);
    }
}