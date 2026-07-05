/// SEMANA 9
/// JOSEPH MAURICIO MONDRAGON MORENO
/// SISTEMAS COMPUTACIONALES

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ProyectoPerritosWCF.Models
{
      /// Representa la respuesta devuelta por la API Dog CEO.
   
    public class DogApiResponse
    {
     
        /// URL de la imagen del perro.
        public string Message { get; set; }

        /// Estado de la respuesta.
        public string Status { get; set; }
    }
}