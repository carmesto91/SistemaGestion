using Microsoft.AspNetCore.Mvc;
using SistemaGestionBussines;
using System.Net;
using WebApiSistemaGestion.Exceptions;
using WebApiSistemaGestion.SistemaGestionData;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
    public class ProductoController : Controller
    {


            [HttpGet]
            public ActionResult<List<Producto>> ListarProductos()
            {
                return AdoProducto.ListarProducto();
            }
            
            [HttpGet("{id}")]
            public ActionResult<Producto> ObtenerProductoporId(int id)
            {
                return AdoProducto.ObtenerProductoPorId(id);
            }
            [HttpPost("{producto}")]
            public IActionResult AgregarProducto([FromBody] Producto productoNuevo)
            {
                try
                {

                    AdoProducto.AgregarProducto(productoNuevo);
                    return base.Created(nameof(AdoProducto.AgregarProducto), new { mensaje = "Producto creado", status = HttpStatusCode.Created, productoNuevo });
                }
                catch (Exception ex)
                {
                    return base.Conflict(new { mensaje = "Error al agregar un producto", status = HttpStatusCode.Conflict });
                }

            }
            [HttpDelete("{id}")]
            public IActionResult BorrarProducto(int id)
            {
                if (id > 0)
                {
                    if (AdoProducto.BorrarUnProductoPorid(id))
                    {
                        return base.Ok(new { mensaje = "Producto borrado", status = 200 });
                    }

                    return base.Conflict(new { mensaje = "No se pudo borrar el Producto" });

                }
                return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
            }

            [HttpPut("{id}")]
            public IActionResult ActualizarProductoPorId(int id, Producto productoActualizado)
            {
                if (id > 0)
                {
                    if (AdoProducto.ActualizarUnProductoPorId(id, productoActualizado))
                    {
                        return base.Ok(new { mensaje = "Producto Actualizado", status = 200, productoActualizado });
                    }
                    return base.Conflict(new { mensaje = "No se pudo Actualizar el Producto" });

                }
                return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
            }
        }

    }
