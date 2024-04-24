using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiProyectoFinal_Coderhouse.Controllers
{


    [ApiController]
    [Route("/api/[controller]")]
    public class VentaController : Controller
    {
        private readonly VentaService ventaService;
        public VentaController(VentaService ventaService)
        {
            this.ventaService = ventaService;
        }


        [HttpGet("{idUsuario}")]
        public ActionResult<List<VentaDTO>> ObtenerVentasPorIdUsuario(int idUsuario)
        {
            if (idUsuario < 0)
            {
                return base.BadRequest(new { mensaje = "el id no puede ser negativo", status = HttpStatusCode.BadRequest });
            }

            try
            {
                return this.ventaService.ObtenerVentasPorIdUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }
        }


        [HttpPost("{idUsuario}")]
        public IActionResult CrearVenta(int idUsuario, [FromBody] List<ProductoDTO> productos)
        {
            if (productos.Count == 0)
            {
                return base.BadRequest(new { mensaje = "No se recibieron los productos necesarios para la venta", status = HttpStatusCode.BadRequest });
            }
            try
            {
                this.ventaService.AgregarNuevaVenta(idUsuario, productos);
                IActionResult result = base.Created(nameof(CrearVenta), new { mensaje = "Venta realizada con exito", status = HttpStatusCode.Created, nuevaVenta = productos });
                return result;
            }
            catch (Exception ex)
            {
                return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }

        }
    }
}
