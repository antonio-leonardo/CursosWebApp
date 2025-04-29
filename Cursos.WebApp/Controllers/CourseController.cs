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

        [HttpGet]
        public ActionResult<List<CourseDto>> GetAll()
        {
            return Ok(_courseService.GetAll());
        }

        [HttpPost]
        public IActionResult Add([FromBody] CourseDto course)
        {
            if (course == null)
            {
                return Problem("Não pode inserir dados nulos", null, 400, "");
            }
            return Ok(this._courseService.Register(course));
        }
    }
}
