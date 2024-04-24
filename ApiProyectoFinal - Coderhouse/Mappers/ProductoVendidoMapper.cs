using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Models;

namespace ApiProyectoFinal_Coderhouse.Mappers
{
    public class ProductoVendidoMapper
    {

        public ProductoVendidoDTO MapearProductoVendidoADTO(ProductoVendido productoVendido)
        {
            ProductoVendidoDTO dto = new ProductoVendidoDTO();
            dto.IdProducto = productoVendido.IdProducto;
            dto.IdVenta = productoVendido.IdVenta;
            dto.Stock = productoVendido.Stock;
            dto.Id = productoVendido.Id;

            return dto;
        }


        public ProductoVendido MapearDTOAProductoVendido(ProductoVendidoDTO dto)
        {
            ProductoVendido productoVendido = new ProductoVendido();
            productoVendido.IdProducto = dto.IdProducto;
            productoVendido.IdVenta = dto.IdVenta;
            productoVendido.Id = dto.Id;
            productoVendido.Stock = dto.Stock;

            return productoVendido;
        }
    }
}
