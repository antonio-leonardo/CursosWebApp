using Cursos.Application.Dto;
using Cursos.Domain.Domain.Models;

namespace Cursos.Application.Interfaces
{
    public interface ICourseService
    {
        List<CourseDto> GetAll();
        Course Register(CourseDto courseDto);
    }
}