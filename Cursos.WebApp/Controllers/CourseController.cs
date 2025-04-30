using Cursos.Application.Dto;
using Cursos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cursos.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            this._courseService = courseService;
        }

        [HttpGet("{id}")]
        public ActionResult<List<CourseDto>> GetById(Guid id)
        {
            return Ok(_courseService.GetById(id));
        }

        [HttpGet]
        public ActionResult<List<CourseDto>> GetAll()
        {
            return Ok(_courseService.GetAll());
        }

        [HttpPost]
        public IActionResult Add([FromBody] CourseDto course)
        {
            if (course == null || (string.IsNullOrWhiteSpace(course.CourseTitle) && string.IsNullOrWhiteSpace(course.InstructorName) && string.IsNullOrWhiteSpace(course.Description) && string.IsNullOrWhiteSpace(course.KnowledgePlatform)))
            {
                return Problem("Não pode inserir dados nulos", null, 400, "Erro");
            }
            try
            {
                return Ok(this._courseService.Register(course));
            }
            catch (Exception ex)
            {
                return Problem($"Não possível fazer inserção do curso devido ao seguinte erro: {ex.Message}", null, 500, "Erro");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] CourseDto course)
        {
            if (course == null || (string.IsNullOrWhiteSpace(course.CourseTitle) && string.IsNullOrWhiteSpace(course.InstructorName) && string.IsNullOrWhiteSpace(course.Description) && string.IsNullOrWhiteSpace(course.KnowledgePlatform)))
            {
                return Problem("Não pode inserir dados nulos", null, 400, "Erro");
            }
            try
            {
                this._courseService.Update(id, course);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem($"Não possível fazer atualização do curso devido ao seguinte erro: {ex.Message}", null, 500, "Erro");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            this._courseService.Delete(id);
            return Ok();
        }
    }
}