using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Models;

namespace ApiProyectoFinal_Coderhouse.Mappers
{
    public class UsuarioMapper
    {
        public UsuarioDTO MapearUsuarioADTO(Usuario usuario)
        {
            UsuarioDTO dto = new UsuarioDTO();

            dto.NombreUsuario = usuario.NombreUsuario;
            dto.Nombre = usuario.Nombre;
            dto.Apellido = usuario.Apellido;
            dto.Mail = usuario.Mail;
            dto.Contraseña = usuario.Contraseña;
            dto.Id = usuario.Id;

            return dto;
        }

        public Usuario MapearDTOAUsuario(UsuarioDTO dto)
        {
            Usuario usuario = new Usuario();

            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.Nombre = dto.Nombre;
            usuario.Apellido = dto.Apellido;
            usuario.Mail = dto.Mail;
            usuario.Contraseña = dto.Contraseña;
            usuario.Id = dto.Id;
            return usuario;
        }
    }
}
