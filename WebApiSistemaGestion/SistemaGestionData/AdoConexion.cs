using System.Data.SqlClient;

namespace WebApiSistemaGestion.SistemaGestionData
{
    public static class AdoConexion
    {
        private static string stringConnection;
        private static SqlConnection connection;
        static AdoConexion()
        {
            AdoConexion.stringConnection = @"Server=.; Database=coderhouse; Trusted_Connection=True;";
        }
        public static SqlConnection GetConnection()
        {
            if (AdoConexion.connection is null || AdoConexion.connection.State == System.Data.ConnectionState.Broken || AdoConexion.connection.State == System.Data.ConnectionState.Closed)
            {
                AdoConexion.connection = new SqlConnection(AdoConexion.stringConnection);
            }


            AdoConexion.connection.Open();

            return AdoConexion.connection;
        }

    }
}
