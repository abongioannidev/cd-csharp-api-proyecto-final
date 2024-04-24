using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiProyectoFinal_Coderhouse.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductoController : Controller
    {
        private readonly ProductoService productoService;
        public ProductoController(ProductoService productoService)
        {
            this.productoService = productoService;
        }


        [HttpGet("{idUsuario}")]
        public ActionResult<List<ProductoDTO>> ObtenerProductosPorIdDeUsuario(int idUsuario)
        {
            if (idUsuario < 0)
            {
                return base.BadRequest(new { message = "el id no puede ser negativo", status = HttpStatusCode.BadRequest });
            }
            try
            {

                return this.productoService.ObtenerProductosPorIdDeUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }

        }

        [HttpPost]
        public IActionResult CrearNuevoProducto([FromBody] ProductoDTO producto)
        {
            try
            {
                this.productoService.CrearNuevoProducto(producto);
                IActionResult result = base.Created(nameof(CreatedAtAction), new { mensaje = "Producto creado con exito", nuevoProducto = producto, status = HttpStatusCode.Created });
                return result;
            }
            catch (Exception ex)
            {
                return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }

        }

        [HttpPut]
        public IActionResult ActualizarProducto([FromBody] ProductoDTO producto)
        {
            try
            {
                this.productoService.ActualizarProducto(producto);
                IActionResult result = base.Accepted(new { mensaje = "Producto actualizado con exito", nuevoProducto = producto, status = HttpStatusCode.Accepted });
                return result;

            }
            catch (Exception ex)
            {
                return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }

        }


        [HttpDelete("{idProducto}")]
        public IActionResult BorrarProductoPorId(int idProducto)
        {
            if (idProducto < 0)
            {
                return base.BadRequest(new { message = "el id no puede ser negativo", status = HttpStatusCode.BadRequest });
            }
            try
            {
                this.productoService.EliminarUnProductoPorId(idProducto);
                IActionResult result = base.Accepted(new { mensaje = "Producto eliminado con exito", status = HttpStatusCode.Accepted });
                return result;

            }
            catch (Exception ex)
            {
                return base.Conflict(new { message = ex.Message, status = HttpStatusCode.Conflict });
            }

        }
    }
}
