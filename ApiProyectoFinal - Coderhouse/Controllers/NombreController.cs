using Microsoft.AspNetCore.Mvc;

namespace ApiProyectoFinal_Coderhouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NombreController : Controller
    {
        [HttpGet]
        public ActionResult<string> ObtenerNombreDelAlumno()
        {
            return "Alejandro Bongioanni";
        }
    }
}
