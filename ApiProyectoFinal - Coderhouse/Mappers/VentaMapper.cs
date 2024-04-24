using ApiProyectoFinal_Coderhouse.DTOs;
using ApiProyectoFinal_Coderhouse.Models;

namespace ApiProyectoFinal_Coderhouse.Mappers
{
    public class VentaMapper
    {

        public VentaDTO MapearVentaADTO(Ventum venta)
        {
            VentaDTO dto = new VentaDTO();

            dto.Comentarios = venta.Comentarios;
            dto.IdUsuario = venta.IdUsuario;
            dto.Id = venta.Id;

            return dto;
        }

        public Ventum MapearDTOAVetum(VentaDTO dto)
        {
            Ventum venta = new Ventum();
            venta.IdUsuario = dto.IdUsuario;
            venta.Id = dto.Id;
            venta.Comentarios = dto.Comentarios;
            return venta;
        }
    }
}
