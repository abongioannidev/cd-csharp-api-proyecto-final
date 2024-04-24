using ApiProyectoFinal_Coderhouse.Database;
using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Mappers;
using ApiProyectoFinal_Coderhouse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiProyectoFinal_Coderhouse.Services
{
    public class ProductoVendidoService
    {
        private readonly CoderContext coderContext;
        private readonly ProductoVendidoMapper productoVendidoMapper;
        public ProductoVendidoService(CoderContext coderContext, ProductoVendidoMapper productoVendidoMapper)
        {
            this.coderContext = coderContext;
            this.productoVendidoMapper = productoVendidoMapper;
        }


        public List<ProductoVendidoDTO>? ObtenerProductosVendidosPorIdUsuario(int idUsuario)
        {

            List<Producto>? productos = this.coderContext.Productos
                .Include(p => p.ProductoVendidos)
                .Where(p => p.IdUsuario == idUsuario)
                .ToList();

            List<ProductoVendido?>? pVendidos = productos
                .Select(p => p.ProductoVendidos
                                        .ToList()
                                        .Find(pv => pv.IdProducto == p.Id))
                .Where(p => !object.ReferenceEquals(p, null))
                .ToList();


            List<ProductoVendidoDTO> dto = pVendidos
                .Select(p => this.productoVendidoMapper.MapearProductoVendidoADTO(p))
                .ToList();
            return dto;
        }

        public bool AgregarProductoVendido(ProductoVendidoDTO productoVendidoDTO)
        {
            ProductoVendido productoVendido = this.productoVendidoMapper.MapearDTOAProductoVendido(productoVendidoDTO);
            EntityEntry<ProductoVendido>? resultado = this.coderContext.ProductoVendidos.Add(productoVendido);
            resultado.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            this.coderContext.SaveChanges();
            return true;
        }
    }

}
