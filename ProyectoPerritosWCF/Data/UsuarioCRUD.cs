/// SEMANA 9
/// JOSEPH MAURICIO MONDRAGON MORENO
/// SISTEMAS COMPUTACIONALES

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;
using ProyectoPerritosWCF.Models;

namespace ProyectoPerritosWCF.Data
{
    public class UsuarioCRUD
    {
        // ======================================================
        // CONSULTA:
        // Consulta un usuario mediante su ID.
        // ======================================================
        public UsuarioCRUDModel ConsultarUsuarioPorId(int idUsuario)
        {
            // Objeto para acceder a la conexión con la base de datos.
            ConexionBD conexionBD = new ConexionBD();

            // Consulta SQL para obtener un usuario por su ID.
            string consulta =
                "SELECT * " +
                "FROM tbl_usuarios_jmmm " +
                "WHERE ID_USUARIO = @idUsuario";

            // Crear la conexión utilizando la clase ConexionBD.
            using (MySqlConnection conexion =
                conexionBD.ObtenerConexion())
            {
                // Abrir la conexión con MySQL.
                conexion.Open();

                // Crear el comando SQL.
                MySqlCommand comando =
                    new MySqlCommand(
                        consulta,
                        conexion);

                // Enviar el valor recibido como parámetro
                // al marcador @idUsuario de la consulta.
                comando.Parameters.AddWithValue(
                    "@idUsuario",
                    idUsuario);

                // Ejecutar la consulta y obtener el resultado.
                MySqlDataReader lector =
                    comando.ExecuteReader();

                // Verificar si la consulta encontró un registro.
                if (lector.Read())
                {
                    // Crear un objeto para almacenar la información.
                    UsuarioCRUDModel usuario =
                        new UsuarioCRUDModel();

                    // Asignar cada columna de la tabla a su propiedad.
                    usuario.ID_USUARIO =
                        Convert.ToInt32(lector["ID_USUARIO"]);

                    usuario.NOMBRE =
                        lector["NOMBRE"].ToString();

                    usuario.APELLIDO_PATERNO =
                        lector["APELLIDO_PATERNO"].ToString();

                    usuario.APELLIDO_MATERNO =
                        lector["APELLIDO_MATERNO"].ToString();

                    usuario.EDAD =
                        Convert.ToInt32(lector["EDAD"]);

                    usuario.GENERO =
                        lector["GENERO"].ToString();

                    usuario.EMAIL =
                        lector["EMAIL"].ToString();

                    usuario.PASSWORD =
                        lector["PASSWORD"].ToString();

                    // Regresar el objeto completamente lleno.
                    return usuario;
                }
            }

            return null;
        }

        // ======================================================
        // CONSULTA GENERAL:
        // Obtiene todos los usuarios registrados.
        // ======================================================
        public List<UsuarioCRUDModel> ObtenerTodosLosUsuarios()
        {
            // Lista donde se almacenarán todos los usuarios
            // obtenidos de la base de datos.
            List<UsuarioCRUDModel> listaUsuarios =
                new List<UsuarioCRUDModel>();

            // Objeto para acceder a la conexión con la base de datos.
            ConexionBD conexionBD = new ConexionBD();

            // Consulta SQL para obtener todos los usuarios.
            string consulta =
                "SELECT * " +
                "FROM tbl_usuarios_jmmm " +
                "ORDER BY ID_USUARIO";
            
            // Crear la conexión utilizando la clase ConexionBD.
            using (MySqlConnection conexion =
                conexionBD.ObtenerConexion())
            {
                // Abrir la conexión con MySQL.
                conexion.Open();

                // Crear el comando SQL.
                MySqlCommand comando =
                    new MySqlCommand(
                        consulta,
                        conexion);

                // Ejecutar la consulta y obtener el resultado.
                MySqlDataReader lector =
                    comando.ExecuteReader();

                // Recorrer todos los registros obtenidos
                // por la consulta.
                while (lector.Read())
                {
                    // Crear un objeto para almacenar
                    // el registro actual.
                    UsuarioCRUDModel usuario =
                        new UsuarioCRUDModel();

                    // Asignar cada columna de la tabla
                    // a su propiedad correspondiente.
                    usuario.ID_USUARIO =
                        Convert.ToInt32(lector["ID_USUARIO"]);

                    usuario.NOMBRE =
                        lector["NOMBRE"].ToString();

                    usuario.APELLIDO_PATERNO =
                        lector["APELLIDO_PATERNO"].ToString();

                    usuario.APELLIDO_MATERNO =
                        lector["APELLIDO_MATERNO"].ToString();

                    usuario.EDAD =
                        Convert.ToInt32(lector["EDAD"]);

                    usuario.GENERO =
                        lector["GENERO"].ToString();

                    usuario.EMAIL =
                        lector["EMAIL"].ToString();

                    usuario.PASSWORD =
                        lector["PASSWORD"].ToString();

                    // Agregar el usuario a la lista.
                    listaUsuarios.Add(usuario);
                }

            }
            return listaUsuarios;
        }

        // ======================================================
        // REGISTRO:
        // Registrar un nuevo usuario.
        // ======================================================
        public bool RegistrarUsuario(UsuarioCRUDModel usuario)
        {
            // Objeto para acceder a la conexión con la base de datos.
            ConexionBD conexionBD =
                new ConexionBD();

            // Consulta SQL para insertar un nuevo usuario.
            string consulta =
                "INSERT INTO tbl_usuarios_jmmm " +
                "(NOMBRE, APELLIDO_PATERNO, APELLIDO_MATERNO, EDAD, GENERO, EMAIL, PASSWORD) " +
                "VALUES " +
                "(@nombre, @apellidoPaterno, @apellidoMaterno, @edad, @genero, @email, @password)";

            // Crear la conexión utilizando la clase ConexionBD.
            using (MySqlConnection conexion =
                conexionBD.ObtenerConexion())
            {
                // Abrir la conexión con MySQL.
                conexion.Open();

                // Crear el comando SQL.
                MySqlCommand comando =
                    new MySqlCommand(
                        consulta,
                        conexion);

                // Asignar los valores recibidos a los parámetros de la consulta.
                comando.Parameters.AddWithValue(
                    "@nombre",
                    usuario.NOMBRE);

                comando.Parameters.AddWithValue(
                    "@apellidoPaterno",
                    usuario.APELLIDO_PATERNO);

                comando.Parameters.AddWithValue(
                    "@apellidoMaterno",
                    usuario.APELLIDO_MATERNO);

                comando.Parameters.AddWithValue(
                    "@edad",
                    usuario.EDAD);

                comando.Parameters.AddWithValue(
                    "@genero",
                    usuario.GENERO);

                comando.Parameters.AddWithValue(
                    "@email",
                    usuario.EMAIL);

                comando.Parameters.AddWithValue(
                    "@password",
                    usuario.PASSWORD);

                // Ejecutar la inserción.
                int filasAfectadas =
                    comando.ExecuteNonQuery();

                // Regresar verdadero si se insertó un registro.
                return filasAfectadas > 0;
            }
        }

        // ======================================================
        // ACTUALIZACIÓN:
        // Actualiza la información de un usuario.
        // ======================================================
        public bool ActualizarUsuario(UsuarioCRUDModel usuario)
        {
            // Objeto para acceder a la conexión con la base de datos.
            ConexionBD conexionBD =
                new ConexionBD();

            // Consulta SQL para actualizar un usuario.
            string consulta =
                "UPDATE tbl_usuarios_jmmm " +
                "SET " +
                "NOMBRE = @nombre, " +
                "APELLIDO_PATERNO = @apellidoPaterno, " +
                "APELLIDO_MATERNO = @apellidoMaterno, " +
                "EDAD = @edad, " +
                "GENERO = @genero, " +
                "EMAIL = @email, " +
                "PASSWORD = @password " +
                "WHERE ID_USUARIO = @idUsuario";

            // Crear la conexión utilizando la clase ConexionBD.
            using (MySqlConnection conexion =
                conexionBD.ObtenerConexion())
            {
                // Abrir la conexión con MySQL.
                conexion.Open();

                // Crear el comando SQL.
                MySqlCommand comando =
                    new MySqlCommand(
                        consulta,
                        conexion);

                // Enviar los datos del objeto usuario
                // a los parámetros de la consulta SQL.
                comando.Parameters.AddWithValue(
                    "@nombre",
                    usuario.NOMBRE);

                comando.Parameters.AddWithValue(
                    "@apellidoPaterno",
                    usuario.APELLIDO_PATERNO);

                comando.Parameters.AddWithValue(
                    "@apellidoMaterno",
                    usuario.APELLIDO_MATERNO);

                comando.Parameters.AddWithValue(
                    "@edad",
                    usuario.EDAD);

                comando.Parameters.AddWithValue(
                    "@genero",
                    usuario.GENERO);

                comando.Parameters.AddWithValue(
                    "@email",
                    usuario.EMAIL);

                comando.Parameters.AddWithValue(
                    "@password",
                    usuario.PASSWORD);

                comando.Parameters.AddWithValue(
                    "@idUsuario",
                    usuario.ID_USUARIO);

                // Ejecutar la actualización del usuario.
                int filasAfectadas =
                    comando.ExecuteNonQuery();

                // Verificar si la actualización fue exitosa.
                return filasAfectadas > 0;
            }
        }

        // ======================================================
        // ELIMINACIÓN:
        // Elimina un usuario mediante su ID.
        // ======================================================
        public bool EliminarUsuario(int idUsuario)
        {
            // Objeto para acceder a la conexión con la base de datos.
            ConexionBD conexionBD =
                new ConexionBD();

            // Consulta SQL para eliminar un usuario.
            string consulta =
                "DELETE FROM tbl_usuarios_jmmm " +
                "WHERE ID_USUARIO = @idUsuario";

            // Crear la conexión utilizando la clase ConexionBD.
            using (MySqlConnection conexion =
                conexionBD.ObtenerConexion())
            {
                // Abrir la conexión con MySQL.
                conexion.Open();

                // Crear el comando SQL.
                MySqlCommand comando =
                    new MySqlCommand(
                        consulta,
                        conexion);

                // Enviar el ID recibido como parámetro.
                comando.Parameters.AddWithValue(
                    "@idUsuario",
                    idUsuario);

                // Ejecutar la eliminación.
                int filasAfectadas =
                    comando.ExecuteNonQuery();

                // Regresar verdadero si se eliminó un registro.
                return filasAfectadas > 0;
            }
        }

        // ======================================================
        // BITÁCORA:
        // Registra el movimiento realizado en el CRUD.
        // ======================================================
        public bool RegistrarMovimientoBitacora(
            int idUsuarioAfectado,
            string tipoMovimiento)
        {
            // IMPLEMENTACIÓN PENDIENTE.
            return false;
        }
    }
}