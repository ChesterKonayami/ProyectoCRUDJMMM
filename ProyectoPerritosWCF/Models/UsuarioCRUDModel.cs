/// SEMANA 9
/// JOSEPH MAURICIO MONDRAGON MORENO
/// SISTEMAS COMPUTACIONALES
using System;

namespace ProyectoPerritosWCF.Models
{
      /// Modelo que representa un registro de la tabla
    /// tbl_usuarios_jmmm.
  
    public class UsuarioCRUDModel
    {
        // Identificador único del usuario.
        public int ID_USUARIO { get; set; }

        // Nombre del usuario.
        public string NOMBRE { get; set; }

        // Apellido paterno.
        public string APELLIDO_PATERNO { get; set; }

        // Apellido materno.
        public string APELLIDO_MATERNO { get; set; }

        // Edad del usuario.
        public int EDAD { get; set; }

        // Género del usuario.
        public string GENERO { get; set; }

        // Correo electrónico.
        public string EMAIL { get; set; }

        // Contraseña.
        public string PASSWORD { get; set; }
    }
}