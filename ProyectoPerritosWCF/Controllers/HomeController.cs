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
        /// Muestra el formulario del CRUD de usuarios.
        [HttpGet]
        public ActionResult Administracion()
        {
            // Objeto encargado de consultar la base de datos.
            UsuarioCRUD crud =
                new UsuarioCRUD();

            // Crear el ViewModel que utilizará la vista.
            AdministracionViewModel modelo =
                new AdministracionViewModel();

            // Inicializar el formulario vacío.
            modelo.Usuario =
                new UsuarioCRUDModel();

            // Obtener todos los usuarios registrados.
            modelo.ListaUsuarios =
                crud.ObtenerTodosLosUsuarios();

            return View(modelo);
        }

        // =====================================================
        // Recibe las acciones del formulario CRUD.
        // =====================================================

        [HttpPost]
        public ActionResult Administracion(
            UsuarioCRUDModel usuario,
            string accion)
        {
            // Objeto para acceder a la base de datos.
            UsuarioCRUD crud =
                new UsuarioCRUD();

            // Crear el ViewModel que se enviará nuevamente a la vista.
            AdministracionViewModel modelo =
                new AdministracionViewModel();

            // Cargar siempre la tabla de usuarios.
            modelo.ListaUsuarios =
                crud.ObtenerTodosLosUsuarios();

            // =====================================================
            // ACCIONES DEL CRUD.
            // =====================================================

            // =========
            // CONSULTAR
            // =========
            if (accion == "Consultar")
            {
                // Buscar el usuario por ID.
                UsuarioCRUDModel usuarioConsultado =
                    crud.ConsultarUsuarioPorId(
                        usuario.ID_USUARIO);

                if (usuarioConsultado != null)
                {
                    // Limpiar los datos enviados por el formulario.
                    ModelState.Clear();

                    // Llenar el formulario con los datos encontrados.
                    modelo.Usuario =
                        usuarioConsultado;
                }
                else
                {
                    // Si no existe, conservar únicamente el ID capturado.
                    modelo.Usuario =
                        usuario;
                }
            }

            // =========
            // REGISTRAR
            // =========
            else if (accion == "Registrar")
            {
                // Registrar el nuevo usuario.
                bool registroExitoso =
                    crud.RegistrarUsuario(usuario);

                if (registroExitoso)
                {
                    // Limpiar los datos enviados por el formulario.
                    ModelState.Clear();

                    // Inicializar nuevamente el formulario.
                    modelo.Usuario =
                        new UsuarioCRUDModel();

                    // Actualizar la tabla de usuarios.
                    modelo.ListaUsuarios =
                        crud.ObtenerTodosLosUsuarios();
                }
                else
                {
                    // Conservar la información capturada en el formulario.
                    modelo.Usuario =
                        usuario;
                }
            }

            // ==========
            // ACTUALIZAR
            // ==========
            else if (accion == "Actualizar")
            {
                // Actualizar la información del usuario.
                bool actualizacionExitosa =
                    crud.ActualizarUsuario(usuario);

                if (actualizacionExitosa)
                {
                    // Limpiar los datos enviados por el formulario.
                    ModelState.Clear();

                    // Inicializar nuevamente el formulario.
                    modelo.Usuario =
                        new UsuarioCRUDModel();

                    // Actualizar la tabla de usuarios.
                    modelo.ListaUsuarios =
                        crud.ObtenerTodosLosUsuarios();
                }
                else
                {
                    // Conservar la información capturada en el formulario.
                    modelo.Usuario =
                        usuario;
                }
            }
            // ========
            // ELIMINAR
            // ========
            else if (accion == "Eliminar")
            {
                // Eliminar el usuario mediante su ID.
                bool eliminacionExitosa =
                    crud.EliminarUsuario(
                        usuario.ID_USUARIO);

                if (eliminacionExitosa)
                {
                    // Limpiar los datos enviados por el formulario.
                    ModelState.Clear();

                    // Inicializar nuevamente el formulario.
                    modelo.Usuario =
                        new UsuarioCRUDModel();

                    // Actualizar la tabla de usuarios.
                    modelo.ListaUsuarios =
                        crud.ObtenerTodosLosUsuarios();
                }
                else
                {
                    // Conservar la información capturada en el formulario.
                    modelo.Usuario =
                        usuario;
                }
            }

            // ======================================
            // OTRAS ACCIONES
            // ======================================
            else
            {
                modelo.Usuario =
                    usuario;
            }

            return View(modelo);
        }
    }
}

