using WebApiSistemaGestion.DTOs;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.Mapper
{
    public class UsuarioMapper
    {
        public static Usuario MapearAUsuario(UsuarioDTO dto)
        {
            Usuario usuario = new Usuario();
            usuario.Id = dto.Id;
            usuario.Nombre = dto.Nombre;
            usuario.Apellido = dto.Apellido;
            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.Password = dto.Password;
            usuario.Mail = dto.Mail;


            return usuario;
        }

        public static UsuarioDTO MapearADTO(Usuario usuario)
        {
            UsuarioDTO dto = new UsuarioDTO();

            dto.Id = usuario.Id;
            dto.Nombre = usuario.Nombre;
            dto.Apellido = usuario.Apellido;
            dto.NombreUsuario = usuario.NombreUsuario;
            dto.Password = usuario.Password;
            dto.Mail = usuario.Mail;

            return dto;

        }
    }
}
