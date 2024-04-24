using ApiProyectoFinal_Coderhouse.Database;
using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Mappers;
using ApiProyectoFinal_Coderhouse.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiProyectoFinal_Coderhouse.Services
{
    public class VentaService
    {
        private readonly CoderContext coderContext;
        private readonly ProductoService productoService;
        private readonly ProductoVendidoService productoVendidoService;
        private readonly VentaMapper ventaMapper;
        public VentaService(CoderContext coderContext, ProductoVendidoService productoVendidoService, ProductoService productoService, VentaMapper ventaMapper)
        {
            this.coderContext = coderContext;
            this.productoVendidoService = productoVendidoService;
            this.productoService = productoService;
            this.ventaMapper = ventaMapper;

        }

        public List<VentaDTO> ObtenerVentasPorIdUsuario(int idUsuario)
        {
            return this.coderContext.Venta
                .Where(v => v.IdUsuario == idUsuario)
                .Select(v => this.ventaMapper.MapearVentaADTO(v))
                .ToList();
        }

        public bool AgregarNuevaVenta(int idUsuario, List<ProductoDTO> productosDTO)
        {

            Ventum venta = new Ventum();
            List<string> nombresDeProductos = productosDTO.Select(p => p.Descripciones).ToList();
            string comentarios = string.Join("-", nombresDeProductos);
            venta.Comentarios = comentarios;
            venta.IdUsuario = idUsuario;

            EntityEntry<Ventum>? resultado = this.coderContext.Venta.Add(venta);
            resultado.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            this.coderContext.SaveChanges();

            this.MarcarComoProductosVendidos(productosDTO, venta.Id);

            this.ActualizarStockDeLosProductosVendidos(productosDTO);


            return true;
        }

        private void MarcarComoProductosVendidos(List<ProductoDTO> productosDTO, int idVenta)
        {
            productosDTO.ForEach(p =>
            {
                ProductoVendidoDTO productoVendidoDTO = new ProductoVendidoDTO();
                productoVendidoDTO.IdProducto = p.Id;
                productoVendidoDTO.IdVenta = idVenta;
                productoVendidoDTO.Stock = p.Stock;

                this.productoVendidoService.AgregarProductoVendido(productoVendidoDTO);

            });
        }

        private void ActualizarStockDeLosProductosVendidos(List<ProductoDTO> productosDTO)
        {
            productosDTO.ForEach(p =>
            {
                ProductoDTO productActual = this.productoService.ObtenerProductoPorIdProducto(p.Id);
                productActual.Stock -= p.Stock;
                this.productoService.ActualizarProducto(productActual);

            });
        }

    }
}
