// SEMANA 9
// JOSEPH MAURICIO MONDRAGON MORENO
// SISTEMAS COMPUTACIONALES 

using System;
using MySql.Data.MySqlClient;

namespace ProyectoPerritosWCF.Data
{
    public class ConexionBD
    {
        /*
         * ============================================
         * CONEXIÓN ORIGINAL (Servidor de la profesora)
         * ============================================
         *
         * Se conserva como referencia para una futura
         * migración cuando el servidor vuelva a estar
         * disponible.
         *
         * private string cadenaConexion =
         * "Server=;" +
         * "Port=;" +
         * "Database=admin_3;" +
         * "Uid=cizcalli_3;" +
         * "Pwd=;";
         */


        // ============================================
        // CONEXIÓN LOCAL (XAMPP)
        // ============================================

        private string cadenaConexion =
            "Server=localhost;" +
            "Port=3306;" +
            "Database=admin_3;" +
            "Uid=root;" +
            "Pwd=;";


        // Método que devuelve una conexión
        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }

        public string ProbarConexion()
        {
            try
            {
                using (MySqlConnection conexion = ObtenerConexion())
                {
                    conexion.Open();
                    return "Conexion exitosa";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Valida si existe un usuario con las credenciales
        // recibidas desde el formulario Login.
        public bool ValidarUsuario(string nombreUsuario, string password)
        {
            string consulta =
                "SELECT COUNT(*) " +
                "FROM TBL_LOGIN_JMMM " +
                "WHERE USUARIO = @usuario " +
                "AND PASSWORD = @password";

            using (MySqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                MySqlCommand comando =
                    new MySqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue("@usuario", nombreUsuario);
                comando.Parameters.AddWithValue("@password", password);

                int cantidad = Convert.ToInt32(comando.ExecuteScalar());

                return cantidad > 0;
            }
        }
    }
}
