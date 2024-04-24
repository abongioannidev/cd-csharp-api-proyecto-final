using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Models;

namespace ApiProyectoFinal_Coderhouse.Mappers
{
    public class ProductoMapper
    {

        public ProductoDTO MapearProductoADTO(Producto producto)
        {
            ProductoDTO dto = new ProductoDTO();
            dto.PrecioVenta = producto.PrecioVenta;
            dto.Descripciones = producto.Descripciones;
            dto.Costo = producto.Costo;
            dto.Stock = producto.Stock;
            dto.IdUsuario = producto.IdUsuario;
            dto.Id = producto.Id;

            return dto;
        }

        public Producto MapearDTOAProducto(ProductoDTO dto)
        {
            Producto producto = new Producto();

            producto.PrecioVenta = dto.PrecioVenta;
            producto.Descripciones = dto.Descripciones;
            producto.Costo = dto.Costo;
            producto.Stock = dto.Stock;
            producto.IdUsuario = dto.IdUsuario;
            producto.Id = dto.Id;
            return producto;
        }
    }
}
