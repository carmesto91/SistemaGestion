using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.SistemaGestionData
{
    public static class AdoProductoVendido
    {
      

        public static List<ProductoVendido> ListarProductoVendido()
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            List<ProductoVendido> lista = new List<ProductoVendido>();
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "SELECT Id, IdProducto, Stock, IdVenta FROM ProductoVendido";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

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

        public static ProductoVendido ObtenerProductoVendidoPorId(int id)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "SELECT * FROM ProductoVendido Where id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();

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

        public static bool AgregarProductoVendido(ProductoVendido productoVendido)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "INSERT INTO ProductoVendido (IdProducto, Stock, IdVenta) values" +
                    "(@idProducto, @stock, @idVenta)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("idProducto", productoVendido.IdProducto);
                command.Parameters.AddWithValue("stock", productoVendido.Stock);
                command.Parameters.AddWithValue("idVenta", productoVendido.IdVenta);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }
        public static bool BorrarUnProductoVendidoPorid(int id)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(stringConnection))
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
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";


            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "UPDATE FROM ProductoVendido SET (IdProducto, Stock, IdVenta) values" +
                    "(@idProducto, @stock, @idVenta) where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("idProducto", productoVendido.IdProducto);
                command.Parameters.AddWithValue("stock", productoVendido.Stock);
                command.Parameters.AddWithValue("idVenta", productoVendido.IdVenta);

                connection.Open();

                return command.ExecuteNonQuery() > 0;


            }



        }
    }
}
