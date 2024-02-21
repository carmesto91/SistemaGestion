namespace WebApiSistemaGestion.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Descripciones { get; set; } = null!;
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }
    }
}
