using Microsoft.AspNetCore.Mvc;

namespace SmartSchoolCode.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Professores: Claudio, Ana, Jaqueline");
        }
    }
}