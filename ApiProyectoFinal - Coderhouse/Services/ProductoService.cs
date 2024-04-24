using ApiProyectoFinal_Coderhouse.Database;
using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Mappers;
using ApiProyectoFinal_Coderhouse.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiProyectoFinal_Coderhouse.Services
{
    public class ProductoService
    {
        private readonly CoderContext coderContext;
        private readonly ProductoMapper productoMapper;
        public ProductoService(CoderContext coderContext, ProductoMapper productoMapper)
        {
            this.coderContext = coderContext;
            this.productoMapper = productoMapper;
        }

        public List<ProductoDTO> ObtenerProductosPorIdDeUsuario(int idUsuario)
        {
            return this.coderContext.Productos
                .Where(p => p.IdUsuario == idUsuario)
                .Select(p => this.productoMapper.MapearProductoADTO(p))
                .ToList();
        }

        public bool CrearNuevoProducto(ProductoDTO producto)
        {
            Producto nuevoProducto = this.productoMapper.MapearDTOAProducto(producto);
            EntityEntry<Producto>? resultado = this.coderContext.Productos.Add(nuevoProducto);
            resultado.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            this.coderContext.SaveChanges();
            return true;
        }

        public bool ActualizarProducto(ProductoDTO productoActualizado)
        {
            Producto? producto = this.coderContext.Productos
                .ToList()
                .Find(p => p.Id == productoActualizado.Id);

            if (producto is null)
            {
                throw new Exception("Producto no encontrado");
            }


            this.ActualizarProducto(producto, productoActualizado);
            EntityEntry<Producto>? resultado = this.coderContext.Productos.Update(producto);
            resultado.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.coderContext.SaveChanges();
            return true;
        }

        private void ActualizarProducto(Producto productoActual, ProductoDTO productoActualizado)
        {
            productoActual.Costo = productoActualizado.Costo;
            productoActual.Stock = productoActualizado.Stock;
            productoActual.PrecioVenta = productoActualizado.PrecioVenta;
            productoActual.Descripciones = productoActualizado.Descripciones;
            productoActual.IdUsuario = productoActualizado.IdUsuario;
        }


        public bool EliminarUnProductoPorId(int idProducto)
        {
            Producto? producto = this.coderContext.Productos
                .ToList()
                .Find(p => p.Id == idProducto);


            if (producto is null)
            {
                throw new Exception("Producto no encontrado");
            }

            EntityEntry<Producto>? resultado = this.coderContext.Productos.Remove(producto);
            resultado.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            this.coderContext.SaveChanges();

            return true;
        }

        public ProductoDTO ObtenerProductoPorIdProducto(int idProducto)
        {
            Producto? producto = this.coderContext.Productos.ToList().Find(p => p.Id == idProducto);

            if (producto is null)
            {
                throw new Exception("Producto no encontrado");
            }

            return this.productoMapper.MapearProductoADTO(producto);
        }
    }
}
