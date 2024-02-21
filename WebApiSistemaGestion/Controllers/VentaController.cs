using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiSistemaGestion.SistemaGestionData;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.Controllers
{
        
        [ApiController]
        [Route("api/[controller]")]
        public class VentaController : Controller
        {


         [HttpGet]
            public ActionResult<List<Venta>> ListarVentas()
            {
                return AdoVenta.ListarVentas();
            }

            [HttpGet("{id}")]
            public ActionResult<Venta> ObtenerVentaporId(int id)
            {
                return AdoVenta.ObtenerVentaPorId(id);
            }
            [HttpPost("{Venta}")]
            public IActionResult AgregarVenta([FromBody] Venta ventaNuevo)
            {
                try
                {

                    AdoVenta.AgregarVenta(ventaNuevo);
                    return base.Created(nameof(AdoVenta.AgregarVenta), new { mensaje = "Venta creado", status = HttpStatusCode.Created, ventaNuevo });
                }
                catch (Exception ex)
                {
                    return base.Conflict(new { mensaje = "Error al agregar una venta", status = HttpStatusCode.Conflict });
                }

            }
            [HttpDelete("{id}")]
            public IActionResult BorrarVenta(int id)
            {
                if (id > 0)
                {
                    if (AdoVenta.BorrarUnaVentaPorid(id))
                    {
                        return base.Ok(new { mensaje = "Venta borrado", status = 200 });
                    }

                    return base.Conflict(new { mensaje = "No se pudo borrar la venta" });

                }
                return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
            }

            [HttpPut("{id}")]
            public IActionResult ActualizarVenta(int id, Venta ventaActualizado)
            {
                if (id > 0)
                {
                    if (AdoVenta.ActualizarUnaVentaPorId(id, ventaActualizado))
                    {
                        return base.Ok(new { mensaje = "Venta Actualizado", status = 200, ventaActualizado });
                    }
                    return base.Conflict(new { mensaje = "No se pudo Actualizar la venta" });

                }
                return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
            }
        }

    }
