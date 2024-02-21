using WebApiSistemaGestion.DTOs;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.Mapper
{
    public class VentaMapper
    {
        public static Venta MapearAVenta(VentaDTO dto)
        {
            Venta venta = new Venta();
            venta.Id = dto.Id;
            venta.Comentarios = dto.Comentarios;
            venta.IdUsuario = dto.IdUsuario;
            return venta;
        }

        public static VentaDTO MapearADTO(Venta venta)
        {
            VentaDTO dto = new VentaDTO();

            dto.Id = venta.Id;
            dto.Comentarios = venta.Comentarios;
            dto.IdUsuario = venta.IdUsuario;

            return dto;

        }
    }
}
