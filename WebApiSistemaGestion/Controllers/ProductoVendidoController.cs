using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiSistemaGestion.SistemaGestionData;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class ProductoVendidoController : Controller
        {


            [HttpGet]
            public ActionResult<List<ProductoVendido>> ListarProductosVendidos()
            {
                return AdoProductoVendido.ListarProductoVendido();
            }

            [HttpGet("{id}")]
            public ActionResult<ProductoVendido> ObtenerProductoporId(int id)
            {
                return AdoProductoVendido.ObtenerProductoVendidoPorId(id);
            }
            [HttpPost("{productoVendido}")]
            public IActionResult AgregarProductoVendido([FromBody] ProductoVendido productoVendidoNuevo)
            {
                try
                {

                    AdoProductoVendido.AgregarProductoVendido(productoVendidoNuevo);
                    return base.Created(nameof(AdoProductoVendido.AgregarProductoVendido), new { mensaje = "Producto Vendido creado", status = HttpStatusCode.Created, productoVendidoNuevo });
                }
                catch (Exception ex)
                {
                    return base.Conflict(new { mensaje = "Error al agregar un producto Vendido", status = HttpStatusCode.Conflict });
                }

            }
            [HttpDelete("{id}")]
            public IActionResult BorrarProductoVendido(int id)
            {
                if (id > 0)
                {
                    if (AdoProductoVendido.BorrarUnProductoVendidoPorid(id))
                    {
                        return base.Ok(new { mensaje = "Producto Vendido borrado", status = 200 });
                    }

                    return base.Conflict(new { mensaje = "No se pudo borrar el ProductoVendido" });

                }
                return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
            }

            [HttpPut("{id}")]
            public IActionResult ActualizarProductoVendidoPorId(int id, ProductoVendido productoVendidoActualizado)
            {
                if (id > 0)
                {
                    if (AdoProductoVendido.ActualizarUnProductoVendidoPorId(id, productoVendidoActualizado))
                    {
                        return base.Ok(new { mensaje = "Producto Vendido Actualizado", status = 200, productoVendidoActualizado });
                    }
                    return base.Conflict(new { mensaje = "No se pudo Actualizar el Producto Vendido" });

                }
                return base.BadRequest(new { status = 400, mensaje = "El id no puede ser negativo" });
            }
        }

    }


