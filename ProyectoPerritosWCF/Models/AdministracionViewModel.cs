/// SEMANA 9
/// JOSEPH MAURICIO MONDRAGON MORENO
/// SISTEMAS COMPUTACIONALES

using System.Collections.Generic;

namespace ProyectoPerritosWCF.Models
{
    /// Modelo utilizado por la vista de Administración.
    /// Contiene la información del formulario y la lista
    /// de usuarios que se mostrarán en la tabla.
    public class AdministracionViewModel
    {
        // ==========================================
        // Información utilizada por el formulario.
        // ==========================================
        public UsuarioCRUDModel Usuario { get; set; }

        // ==========================================
        // Lista utilizada para llenar la tabla
        // de usuarios registrados.
        // ==========================================
        public List<UsuarioCRUDModel> ListaUsuarios { get; set; }

        // ==========================================
        // Información del último movimiento realizado.      
        // ==========================================
        public string Movimiento { get; set; }

        public string Fecha { get; set; }

        public string Hora { get; set; }
    }
}