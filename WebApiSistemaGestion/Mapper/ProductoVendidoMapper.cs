using WebApiSistemaGestion.DTOs;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.Mapper
{
    public class ProductoVendidoMapper
    {

        public static ProductoVendido MapearAProductoVendido(ProductoVendidoDTO dto)
        {
            ProductoVendido productoVendido = new ProductoVendido();
            productoVendido.Id = dto.Id;
            productoVendido.IdVenta = dto.IdVenta;
            productoVendido.IdProducto = dto.IdProducto;
            productoVendido.Stock=dto.Stock;

            return productoVendido;
        }

        public static ProductoVendidoDTO MapearADTO(ProductoVendido productoVendido)
        {
            ProductoVendidoDTO dto = new ProductoVendidoDTO();

            dto.Id = productoVendido.Id;
            dto.IdVenta = productoVendido.IdVenta;
            dto.Stock = productoVendido.Stock;
            dto.IdProducto = productoVendido.IdProducto;

            return dto;

        }
    }
}
}
