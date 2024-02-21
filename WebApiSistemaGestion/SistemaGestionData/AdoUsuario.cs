using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebApiSistemaGestion.Exceptions;
using WebApiSistemaGestion.SistemaGestionEntities;

namespace WebApiSistemaGestion.SistemaGestionData
{
    public static class AdoUsuario
    {
       
        public static List<Usuario> ListarUsuarios()
        {
            try
            {
                
                List<Usuario> lista = new List<Usuario>();
                using (SqlConnection connection = AdoConexion.GetConnection())
                {
                    string query = "SELECT Id, Nombre, Apellido, NombreUsuario, Contraseña, Mail FROM Usuario";
                    SqlCommand command = new SqlCommand(query, connection);

                   

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
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
            catch (Exception ex)
            {
                throw new DataBaseException("Error al obtener todos los usuarios", ex);
            }


        
    }

            public static Usuario ObtenerUsuarioPorId(int id)
        {
                try
                {
                   
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "SELECT * FROM Usuario Where id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
              

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
        catch (Exception ex)
        {
            throw new DataBaseException("Error al obtenene un usuario por id", ex);
        }

    }

    public static bool AgregarUsuario(Usuario usuario)
        {
          
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) values" +
                    "(@nombre, @apellido, @nombreUsuario, @password, @mail)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("nombre", usuario.Nombre);
                command.Parameters.AddWithValue("apellido", usuario.Apellido);
                command.Parameters.AddWithValue("nombreUsuario", usuario.NombreUsuario);
                command.Parameters.AddWithValue("password", usuario.Password);
                command.Parameters.AddWithValue("mail", usuario.Mail);
            

                return command.ExecuteNonQuery() > 0;
            }
        }
        public static bool BorrarUnUsuarioPorid(int id)
        {
        
            using (SqlConnection connection = AdoConexion.GetConnection())
            {
                string query = "DELETE FROM Usuario Where id= @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("id", id);
                

                return command.ExecuteNonQuery() >0;


                 }   
        }
        
        public static bool ActualizarUnUsuarioPorId(int id, Usuario usuario)
        {
   
           
            using (SqlConnection connection = AdoConexion.GetConnection())
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

              

                return command.ExecuteNonQuery() > 0;


            }



        }

    }
}
