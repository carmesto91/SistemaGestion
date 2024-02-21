using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSistemaGestion.Exceptions;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.SistemaGestionData
{
    public static class AdoProductoVendido
    {
       

        public static List<ProductoVendido> ListarProductoVendido()
        {
            try { 
           
            List<ProductoVendido> lista = new List<ProductoVendido>();
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "SELECT Id, IdProducto, Stock, IdVenta FROM ProductoVendido";
                SqlCommand command = new SqlCommand(query, connection);


                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var productoVendido = new ProductoVendido();
                        productoVendido.Id = Convert.ToInt32(reader["Id"]);
                        productoVendido.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                        productoVendido.Stock = Convert.ToInt32(reader["Stock"]);
                        productoVendido.IdVenta = Convert.ToInt32(reader["IdVenta"]);
                        lista.Add(productoVendido);
                    }


                }
                return lista;



            }
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error al obtener todos los Productos Vendidos", ex);
            }

        }

        public static ProductoVendido ObtenerProductoVendidoPorId(int id)
        {
            try
            {
                
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "SELECT * FROM ProductoVendido Where id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
            

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int idObtenido = Convert.ToInt32(reader["id"]);
                    int idProducto = Convert.ToInt32(reader["IdProducto"]);
                    int stock = Convert.ToInt32(reader["Stock"]);
                    int idVenta = Convert.ToInt32(reader["IdVenta"]);

                    ProductoVendido productoVendido = new ProductoVendido(id, idProducto, stock, idVenta);

                    return productoVendido;


                }
                throw new Exception("Id de producto no encontrada");


            }
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error al obtener producto vendido por id", ex);
            }
        }

        public static bool AgregarProductoVendido(ProductoVendido productoVendido)
        {
            
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "INSERT INTO ProductoVendido (IdProducto, Stock, IdVenta) values" +
                    "(@idProducto, @stock, @idVenta)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("idProducto", productoVendido.IdProducto);
                command.Parameters.AddWithValue("stock", productoVendido.Stock);
                command.Parameters.AddWithValue("idVenta", productoVendido.IdVenta);
            

                return command.ExecuteNonQuery() > 0;
            }
        }
        public static bool BorrarUnProductoVendidoPorid(int id)
        {
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "DELETE FROM ProductoVendido Where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();

                return command.ExecuteNonQuery() > 0;


            }
        }

        public static bool ActualizarUnProductoVendidoPorId(int id, ProductoVendido productoVendido)
        {
           
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "UPDATE FROM ProductoVendido SET (IdProducto, Stock, IdVenta) values" +
                    "(@idProducto, @stock, @idVenta) where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("idProducto", productoVendido.IdProducto);
                command.Parameters.AddWithValue("stock", productoVendido.Stock);
                command.Parameters.AddWithValue("idVenta", productoVendido.IdVenta);

                return command.ExecuteNonQuery() > 0;


            }



        }
    }
}
