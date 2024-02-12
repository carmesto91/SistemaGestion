using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.SistemaGestionData
{
    public static class AdoUsuario
    {
       
        public static List<Usuario> ListarUsuarios()
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "SELECT Id, Nombre, Apellido, NombreUsuario, Contraseña, Mail FROM Usuario";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read()) { 
                    var usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Password = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);
                    lista.Add(usuario);
                    }


                }
                return lista;



            }
        }

            public static Usuario ObtenerUsuarioPorId(int id)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "SELECT * FROM Usuario Where id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    int idObtenido = Convert.ToInt32(reader["id"]);
                    string nombre = reader.GetString(1);
                    string apellido = reader.GetString(2);
                    string nombreUsuario = reader.GetString(3);
                    string password = reader.GetString(4);
                    string email = reader.GetString(5);

                    Usuario usuario = new Usuario(id, nombre, apellido, nombreUsuario, password, email);

                    return usuario;


                }
                throw new Exception("Id de usuario no encontrada");


            }
        }

        public static bool AgregarUsuario(Usuario usuario)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) values" +
                    "(@nombre, @apellido, @nombreUsuario, @password, @mail)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("nombre", usuario.Nombre);
                command.Parameters.AddWithValue("apellido", usuario.Apellido);
                command.Parameters.AddWithValue("nombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("password", usuario.Password);
                command.Parameters.AddWithValue("mail", usuario.Mail);
                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }
        public static bool BorrarUnUsuarioPorid(int id)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";


            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "DELETE FROM Usuario Where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                connection.Open();

                return command.ExecuteNonQuery() >0;


                 }   
        }
        
        public static bool ActualizarUnUsuarioPorId(int id, Usuario usuario)
        {
            string stringConnection = "Server=LAPTOP-KT3LRP0Q\\MSSQLSERVER01;Database=TodoHerramientas;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(stringConnection))
            {
                string query = "UPDATE FROM Usuario SET (Nombre, Apellido, NombreUsuario, Contraseña, Mail) values" +
                    "(@nombre, @apellido, @nombreUsuario, @password, @mail) where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("nombre", usuario.Nombre);
                command.Parameters.AddWithValue("apellido", usuario.Apellido);
                command.Parameters.AddWithValue("nombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("password", usuario.Password);
                command.Parameters.AddWithValue("mail", usuario.Mail);

                connection.Open();

                return command.ExecuteNonQuery() > 0;


            }



        }

    }
}
