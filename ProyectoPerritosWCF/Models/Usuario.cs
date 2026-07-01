/// SEMANA 9
/// JOSEPH MAURICIO MONDRAGON MORENO
/// SISTEMAS COMPUTACIONALES

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoPerritosWCF.Models
{
    // Clase que representa la información de un usuario
    // almacenado dentro de la tabla TBL_LOGIN_JMMM.
    public class Usuario
    {
        // Identificador único del usuario dentro de la base de datos.
        public int IdUsuario { get; set; }

        // Nombre utilizado por el usuario para iniciar sesión.
        public string NombreUsuario { get; set; }

        // Contraseña asociada al usuario.
        public string Password { get; set; }
    }
}