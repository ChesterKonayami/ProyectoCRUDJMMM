/// SEMANA 9
/// JOSEPH MAURICIO MONDRAGON MORENO
/// SISTEMAS COMPUTACIONALES

using Newtonsoft.Json;
using ProyectoPerritosWCF.Data;
using ProyectoPerritosWCF.DogServiceReference;
using ProyectoPerritosWCF.Models;
using ProyectoPerritosWCF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoPerritosWCF.Controllers
{
    public class HomeController : Controller
    {
        /// Muestra la pantalla de Login.
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// Recibe los datos enviados desde el formulario Login.
        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario) ||
                string.IsNullOrWhiteSpace(usuario.Password))
            {
                ViewBag.MensajeError =
                    "Debe ingresar usuario y contraseña.";

                return View();
            }

            ConexionBD conexion = new ConexionBD();

            bool usuarioValido =
                conexion.ValidarUsuario(
                    usuario.NombreUsuario,
                    usuario.Password);

            if (usuarioValido)
            {
                return RedirectToAction("Administracion");
            }

            ViewBag.MensajeError =
                "Usuario o contraseña incorrectos.";

            return View();
        
            

            // IMPLEMENTACIÓN TEMPORAL
            //return RedirectToAction("Perritos");
        }

        /// Vista de documentación del proyecto.
        public ActionResult About()
        {
            ViewBag.Message = "Documentación del proyecto.";

            return View();
        }


        /// Vista de contacto (plantilla MVC).
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// Pantalla que consume la API Dog CEO
        /// y muestra una imagen aleatoria de perro.
        public ActionResult Perritos()
        {
            DogServiceClient cliente =
                new DogServiceClient();

            string respuestaJson =
                cliente.ObtenerImagenAleatoriaPerro();

            DogApiResponse perro =
                JsonConvert.DeserializeObject<DogApiResponse>(
                    respuestaJson);

            return View(perro);
        }

        /// Panel de Administración.
        /// Aquí se implementará el sistema CRUD
        /// para la administración de usuarios.
        public ActionResult Administracion()
        {
            return View();
        }
    }
}