namespace WebApiSistemaGestion.Exceptions
{
    public class UsuarioNoEncontradoException : Exception
    {
        public UsuarioNoEncontradoException(string? message) : base(message)
        {
        }

        public UsuarioNoEncontradoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
