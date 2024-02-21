using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using WebApiSistemaGestion.SistemaGestionData;
using WebApiSistemaGestion.SistemaGestionEntities;
using WebApiSistemaGestion.Exceptions;

namespace SistemaGestionBussines
{
    public static class UsuarioBussines
    {

        public static List<Usuario> ObtenerTodosLosUsuarios()
        {
            return AdoUsuario.ListarUsuarios();
        }

        public static Usuario ObtenerUsuarioPorNombreDeUsuario(string nombreDeUsuario)
        {
            List<Usuario> usuarios = AdoUsuario.ListarUsuarios();

            Usuario? usuarioBuscado = usuarios.Find(u => u.NombreUsuario == nombreDeUsuario);
            if (usuarioBuscado is null)
            {
                throw new Exception("Usuario no encontrado");
            }

            return usuarioBuscado;
        }

        public static Usuario? ObtenerUsuarioPorUsuarioYPassword(string usuario, string password)
        {
            List<Usuario> usuarios = AdoUsuario.ListarUsuarios();



            Usuario? usuarioBuscado = usuarios.Find(u => u.NombreUsuario == usuario && u.Password == password);


            if (usuarioBuscado is null)
            {
                throw new UsuarioNoEncontradoException("Usuario no encontrado");
            }
            return usuarioBuscado;
        }





    }
}
