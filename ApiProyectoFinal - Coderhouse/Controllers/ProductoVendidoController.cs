using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiProyectoFinal_Coderhouse.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductoVendidoController : Controller
    {
        private readonly ProductoVendidoService productoVendidoService;
        public ProductoVendidoController(ProductoVendidoService productoVendidoService)
        {
            this.productoVendidoService = productoVendidoService;
        }


        [HttpGet("{idUsuario}")]
        public ActionResult<List<ProductoVendidoDTO>> ObtenerProductosVendidosPorIdUsuario(int idUsuario)
        {
            if (idUsuario < 0)
            {
                return base.BadRequest(new { message = "el id no puede ser negativo", status = HttpStatusCode.BadRequest });
            }
            try
            {
                return this.productoVendidoService.ObtenerProductosVendidosPorIdUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }

        }
    }
}
