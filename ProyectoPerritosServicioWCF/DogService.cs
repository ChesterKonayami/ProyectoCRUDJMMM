/// SEMANA 9
/// JOSEPH MAURICIO MONDRAGON MORENO
/// SISTEMAS COMPUTACIONALES

using System.Net;

namespace ProyectoPerritosServicioWCF
{
    /// Servicio WCF encargado de consumir la API Dog CEO.
    public class DogService : IDogService
    {
        /// Obtiene una imagen aleatoria de perro desde Dog CEO.
        public string ObtenerImagenAleatoriaPerro()
        {
            string direccionApi =
            "https://dog.ceo/api/breeds/image/random";

        WebClient clienteWeb = new WebClient();

            string respuestaJson =
                clienteWeb.DownloadString(direccionApi);

            return respuestaJson;
        }
    }

}
