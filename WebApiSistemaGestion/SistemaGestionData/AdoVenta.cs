using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.SistemaGestionData
{
    public static class AdoVenta
    {
        
        public static List<Venta> ListarVentas()
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            List<Venta> lista = new List<Venta>();
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "SELECT Id, Comentarios, IdUsuario FROM Usuario";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

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

        public static Venta ObtenerVentaPorId(int id)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "SELECT * FROM Venta Where id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();

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

        public static bool AgregarVenta(Venta venta)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "INSERT INTO Venta (Comentarios, IdUsuario) values" +
                    "(@comentarios, @idUsuario)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("comentarios", venta.Comentarios);
                command.Parameters.AddWithValue("idUsuario", venta.Id);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }
        public static bool BorrarUnaVentaPorid(int id)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";


            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "DELETE FROM Venta Where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();

                return command.ExecuteNonQuery() > 0;


            }
        }

        public static bool ActualizarUnaVentaPorId(int id, Venta venta)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";


            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "UPDATE FROM Venta SET (Comentarios, IdUsuario) values" +
                    "(@comentarios, @idUsuario) where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("comentarios", venta.Comentarios);
                command.Parameters.AddWithValue("idUsuario", venta.IdUsuario);
                
                connection.Open();

                return command.ExecuteNonQuery() > 0;


            }



        }

    }
}
