using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiProyectoFinal_Coderhouse.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioService usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }


        [HttpGet("{nombreDeUsuario}")]
        public ActionResult<UsuarioDTO> ObtenerUsuarioPorNombreDeUsuario(string nombreDeUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreDeUsuario))
            {
                return base.BadRequest(new { message = "El nombre de usuario no puede ser una cadena de caracteres vacia o con espacios" });
            } 
            try
            {
                UsuarioDTO usuarioDTO = usuarioService.ObtenerUsuarioPorNombreDeUsuario(nombreDeUsuario);
                return usuarioDTO;
            }
            catch
            {
                return base.Conflict(new { message = "No se pudo obtener un usuario en base a los datos proporcionados", status = HttpStatusCode.Conflict });
            }
        }

        [HttpGet("{usuario}/{password}")]
        public ActionResult<UsuarioDTO> ObtenerUsuarioPorUsuarioYPassword(string usuario, string password)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                return base.BadRequest(new { message = "El usuario o el password no puede ser una cadena de caracteres vacia o con espacios", status = HttpStatusCode.BadRequest });
            }

            try
            {
                return this.usuarioService.ObtenerUsuarioPorUsuarioYPassword(usuario, password);
            }
            catch
            {
                return base.Unauthorized(new { message = "No se pudo obtener un usuario en base a los datos proporcionados", status = HttpStatusCode.Unauthorized });
            }

        }


        [HttpPost]
        public IActionResult CrearUsuario([FromBody] UsuarioDTO usuario)
        {
            try
            {
                this.usuarioService.AgregarNuevoUsuario(usuario);
                IActionResult result = base.Created(nameof(CrearUsuario), new { mensaje = "Usuario creado con exito", status = HttpStatusCode.Created, nuevoUsuario = usuario });
                return result;

            }
            catch (Exception ex)
            {
                return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }

        }
        [HttpPut]
        public IActionResult ActualizarUsuario([FromBody] UsuarioDTO usuario)
        {

            try
            {
                this.usuarioService.ActualizarUsuario(usuario);
                IActionResult result = base.Accepted(new { mensaje = "Usuario actualizado con exito", status = HttpStatusCode.Accepted, nuevoUsuario = usuario });
                return result;
            }
            catch (Exception ex)
            {
                return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }

        }
    }
}
