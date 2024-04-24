using ApiProyectoFinal_Coderhouse.Database;
using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Mappers;
using ApiProyectoFinal_Coderhouse.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiProyectoFinal_Coderhouse.Services
{
    public class UsuarioService
    {
        private readonly CoderContext coderContext;
        private readonly UsuarioMapper usuarioMapper;

        public UsuarioService(CoderContext coderContext, UsuarioMapper usuarioMapper)
        {
            this.coderContext = coderContext;
            this.usuarioMapper = usuarioMapper;
        }

        public UsuarioDTO? ObtenerUsuarioPorNombreDeUsuario(string nombreDeUsuario)
        {
            List<Usuario> usuarios = this.ObtenerTodosLosElementos();

            Usuario? usuarioBuscado = usuarios.Find(u => u.NombreUsuario == nombreDeUsuario);

            if (usuarioBuscado is not null)
            {
                return this.usuarioMapper.MapearUsuarioADTO(usuarioBuscado!);
            }
            throw new Exception("Usuario no encontrado");
        }

        private List<Usuario> ObtenerTodosLosElementos()
        {
            return this.coderContext.Usuarios.ToList();
        }

        public UsuarioDTO? ObtenerUsuarioPorUsuarioYPassword(string usuario, string password)
        {
            List<Usuario> usuarios = this.ObtenerTodosLosElementos();

            Usuario? usuarioBuscado = usuarios.Find(u => u.NombreUsuario == usuario && u.Contraseña == password);

            if (usuarioBuscado is not null)
            {
                return this.usuarioMapper.MapearUsuarioADTO(usuarioBuscado);
            }
            throw new Exception("Usuario no encontrado");
        }

        public bool AgregarNuevoUsuario(UsuarioDTO nuevoUsuario)
        {
            Usuario usuario = this.usuarioMapper.MapearDTOAUsuario(nuevoUsuario);
            EntityEntry<Usuario>? resultado = this.coderContext.Usuarios.Add(usuario);
            resultado.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            this.coderContext.SaveChanges();
            return true;
        }


        public bool ActualizarUsuario(UsuarioDTO usuarioActualizado)
        {
            Usuario? usuario = this.coderContext.Usuarios
                .Where(u => u.Id == usuarioActualizado.Id)
                .FirstOrDefault();

            if (usuario is null)
            {
                throw new Exception("Usuario no encontrado");
            }

            this.ActualizarUsuario(usuario, usuarioActualizado);
            EntityEntry<Usuario>? resultado = this.coderContext.Usuarios.Update(usuario);
            resultado.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.coderContext.SaveChanges();
            return true;
        }


        private void ActualizarUsuario(Usuario usuarioActual, UsuarioDTO nuevoUsuario)
        {
            usuarioActual.NombreUsuario = nuevoUsuario.NombreUsuario;
            usuarioActual.Mail = nuevoUsuario.Mail;
            usuarioActual.Contraseña = nuevoUsuario.Contraseña;
            usuarioActual.Nombre = nuevoUsuario.Nombre;
            usuarioActual.Apellido = nuevoUsuario.Apellido;
        }
    }
}
