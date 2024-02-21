using Microsoft.AspNetCore.Mvc;
using WebApiSistemaGestion.SistemaGestionEntities;
using WebApiSistemaGestion.SistemaGestionData;
using WebApiSistemaGestion.Exceptions;
using System.Net;
using SistemaGestionBussines;

namespace WebApiSistemaGestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        

        [HttpGet]
        public ActionResult<List<Usuario>> ListarUsuarios()
        {
            return AdoUsuario.ListarUsuarios();
        }
        [HttpGet("{usuario}/{password}")]

        public ActionResult<Usuario> ObtenerUsuarioPorNombreYPassword(string usuario, string password)
        {
            try
            {

                return UsuarioBussines.ObtenerUsuarioPorUsuarioYPassword(usuario, password);
            }
            catch (DataBaseException ex)
            {
                return base.Conflict(new { error = ex.Message, status = HttpStatusCode.InternalServerError });
            }
            catch (UsuarioNoEncontradoException ex)
            {
                return base.Conflict(new { error = ex.Message, status = HttpStatusCode.NoContent });
            }
            catch (Exception ex)
            {
                return base.Conflict(new { error = ex.Message, status = HttpStatusCode.Conflict });
            }


        }
        [HttpGet ("{id}")]
        public ActionResult<Usuario> ObtenerUsuarioporId(int id)
        {
            return AdoUsuario.ObtenerUsuarioPorId(id);
        }
        [HttpPost ("{usuario}")]
        public IActionResult AgregarUsuario([FromBody] Usuario usuarioNuevo)
        {
            try
            {

                AdoUsuario.AgregarUsuario(usuarioNuevo);
                return base.Created(nameof(AdoUsuario.AgregarUsuario), new { mensaje = "Usuario creado", status = HttpStatusCode.Created, usuarioNuevo });
            }
            catch (Exception ex)
            {
                return base.Conflict(new { mensaje = "Error al agregar un usuario", status = HttpStatusCode.Conflict });
            }

        }
        [HttpDelete("{id}")]
        public IActionResult BorrarUsuario(int id)
        {
            if (id > 0)
            {
                if (AdoUsuario.BorrarUnUsuarioPorid(id))
                {
                    return base.Ok(new { mensaje = "Usuario borrado", status = 200 });
                }

                return base.Conflict(new { mensaje = "No se pudo borrar el usuario" });

            }
            return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarUsuarioPorId(int id, Usuario usuarioActualizado)
        {
            if (id > 0)
            {
                if (AdoUsuario.ActualizarUnUsuarioPorId(id, usuarioActualizado))
                {
                    return base.Ok(new { mensaje = "Usuario Actualizado", status = 200, usuarioActualizado });
                }
                return base.Conflict(new { mensaje = "No se pudo Actualizar el Usuario" });

            }
            return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
        }
    }

}

