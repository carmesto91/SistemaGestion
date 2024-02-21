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
    public static class AdoVenta
    {
        
        public static List<Venta> ListarVentas()
        {
            try
            {
                
            List<Venta> lista = new List<Venta>();
                using (SqlConnection connection = AdoConexion.GetConnection())
                {
                    string query = "SELECT Id, Comentarios, IdUsuario FROM Usuario";
                    SqlCommand command = new SqlCommand(query, connection);

                    

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var venta = new Venta();
                            venta.Id = Convert.ToInt32(reader["Id"]);
                            venta.Comentarios = reader.GetString(1);
                            venta.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);

                            lista.Add(venta);
                        }


                    }
                    return lista;
                }
                }
            catch (Exception ex)
            {
                throw new DataBaseException("Error al obtener todoas las ventas", ex);
            }

        }
        

        public static Venta ObtenerVentaPorId(int id)
        {
        try
        {
            
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "SELECT * FROM Venta Where id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int idObtenido = Convert.ToInt32(reader["id"]);
                    string comentarios = reader.GetString(1);
                    int idUsuario = Convert.ToInt32(reader["IdUsuario"]);

                    Venta venta = new Venta(id, comentarios, idUsuario);

                    return venta;


                }
                throw new Exception("Id de venta no encontrada");
            }
        }
        catch (Exception ex)
        {
            throw new DataBaseException("Error al obtener la venta por id", ex);
        }

    
        }

        public static bool AgregarVenta(Venta venta)
        {
            
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "INSERT INTO Venta (Comentarios, IdUsuario) values" +
                    "(@comentarios, @idUsuario)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("comentarios", venta.Comentarios);
                command.Parameters.AddWithValue("idUsuario", venta.Id);
              

                return command.ExecuteNonQuery() > 0;
            }
        }
        public static bool BorrarUnaVentaPorid(int id)
        {
           
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "DELETE FROM Venta Where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                

                return command.ExecuteNonQuery() > 0;


            }
        }

        public static bool ActualizarUnaVentaPorId(int id, Venta venta)
        {

            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "UPDATE FROM Venta SET (Comentarios, IdUsuario) values" +
                    "(@comentarios, @idUsuario) where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("comentarios", venta.Comentarios);
                command.Parameters.AddWithValue("idUsuario", venta.IdUsuario);
                

                return command.ExecuteNonQuery() > 0;


            }



        }

    }
}
