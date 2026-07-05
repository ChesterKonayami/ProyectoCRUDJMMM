/// SEMANA 9
/// JOSEPH MAURICIO MONDRAGON MORENO
/// SISTEMAS COMPUTACIONALES

using ProyectoPerritosWCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;

namespace ProyectoPerritosWCF.Services
{
    /// Servicio encargado de consumir la API pública Dog CEO.
    public class DogService
    {
        /// Realiza una solicitud a la API Dog CEO para obtener
        /// una imagen aleatoria de perro.

        /// Objeto DogApiResponse con la URL de la imagen y
        /// el estado de la respuesta devuelta por la API.
        public DogApiResponse ObtenerImagenAleatoriaPerro()
        {
            /// Dirección del endpoint de la API que genera
            /// una imagen aleatoria de perro.
            string direccionApi = "https://dog.ceo/api/breeds/image/random";

            /// Cliente web utilizado para enviar la solicitud HTTP
            /// al servidor de la API.
            WebClient clienteWeb = new WebClient();

            /// Almacena la respuesta JSON devuelta por la API
            /// después de realizar la petición GET.
            string respuestaJson = clienteWeb.DownloadString(direccionApi);

            /// Convierte la respuesta JSON en un objeto de tipo
            /// DogApiResponse para facilitar el acceso a sus propiedades.
            DogApiResponse respuestaApi =
                JsonConvert.DeserializeObject<DogApiResponse>(respuestaJson);

            // Regresa el objeto generado al controlador.
            return respuestaApi;
        }
    }
    }
