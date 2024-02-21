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
    public static class AdoProducto
    {
      
        public static List<Producto> ListarProducto()
        {
            try
            {

                List<Producto> lista = new List<Producto>();
                using (SqlConnection connection = AdoConexion.GetConnection())
                {
                    string query = "SELECT Id, Descripciones, Costo, PrecioVenta, Stock, IdUsuario FROM Producto";
                    SqlCommand command = new SqlCommand(query, connection);

                   

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var producto = new Producto();
                            producto.Id = Convert.ToInt32(reader["Id"]);
                            producto.Descripcion = reader.GetString(1);
                            producto.Costo = Convert.ToDouble(reader["Costo"]);
                            producto.PrecioVenta = Convert.ToDouble(reader["PrecioVenta"]);
                            producto.Stock = reader.GetInt32(4);
                            producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                            lista.Add(producto);
                        }


                    }
                    return lista;



                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error al obtener todos los Productos", ex);
            }

        }

        public static Producto ObtenerProductoPorId(int id)
        {
                try
                {
                    
                    using (SqlConnection connection = AdoConexion.GetConnection())
                    {
                        string query = "SELECT * FROM Producto Where id = @id";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("id", id);
                      

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            int idObtenido = Convert.ToInt32(reader["id"]);
                            string descripcion = reader.GetString(1);
                            double costo = Convert.ToDouble(reader["Costo"]);
                            double precioVenta = Convert.ToDouble(reader["PrecioVenta"]);
                            int stock = reader.GetInt32(4);
                            int idUsuario = Convert.ToInt32(reader["IdUsuario"]);

                            Producto producto = new Producto(id, descripcion, costo, precioVenta, stock, idUsuario);

                            return producto;


                        }
                        throw new Exception("Id de producto no encontrada");


                    }
                }
                catch (Exception ex)
                {
                    throw new DataBaseException("Error al obtener un producto por id", ex);
                }

            }

            public static bool AgregarProducto(Producto producto)
        {
            
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) values" +
                    "(@descripcion, @costo, @precioVenta, @stock, @idUsuario)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("costo", producto.Costo);
                command.Parameters.AddWithValue("precioVenta", producto.PrecioVenta);
                command.Parameters.AddWithValue("stock", producto.Stock);
                command.Parameters.AddWithValue("idUsuario", producto.IdUsuario);
              

                return command.ExecuteNonQuery() > 0;
            }
        }
        public static bool BorrarUnProductoPorid(int id)
        {
            
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "DELETE FROM Producto Where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
              

                return command.ExecuteNonQuery() > 0;


            }
        }

        public static bool ActualizarUnProductoPorId(int id, Producto producto)
        {
            
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "UPDATE FROM Producto SET (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) values" +
                    "(@descripcion, @costo, @precioVenta, @stock, @idUsuario) where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("costo", producto.Costo);
                command.Parameters.AddWithValue("precioVenta", producto.PrecioVenta);
                command.Parameters.AddWithValue("stock", producto.Stock);
                command.Parameters.AddWithValue("idUsuario", producto.IdUsuario);
                
               

                return command.ExecuteNonQuery() > 0;


            }



        }

    }
}

    

