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
using System.Net.Mail;


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
                // Validar que se haya capturado un ID.
                if (usuario.ID_USUARIO == 0)
                {
                    // Limpiar los datos almacenados en el ModelState.
                    ModelState.Clear();

                    ViewBag.ErrorID =
                        "Debe ingresar un ID para realizar la consulta.";

                    // Inicializar nuevamente el formulario.
                    modelo.Usuario =
                        new UsuarioCRUDModel();

                    return View(modelo);
                }

                // Validar que el ID sea mayor que cero.
                if (usuario.ID_USUARIO < 0)
                {
                    // Limpiar los datos almacenados en el ModelState.
                    ModelState.Clear();

                    ViewBag.ErrorID =
                    "El ID debe ser mayor que cero.";

                    // Inicializar nuevamente el formulario.
                    modelo.Usuario =
                        new UsuarioCRUDModel();

                    // Conservar únicamente el ID capturado.
                    modelo.Usuario.ID_USUARIO =
                    usuario.ID_USUARIO;

                    return View(modelo);
                }

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
                    // Limpiar los datos almacenados en el ModelState.
                    ModelState.Clear();

                    // Informar que el usuario no existe.
                    ViewBag.ErrorID =
                        "No existe un usuario con el ID especificado.";

                    // Conservar únicamente el ID capturado.
                    modelo.Usuario =
                        new UsuarioCRUDModel();

                    modelo.Usuario.ID_USUARIO =
                        usuario.ID_USUARIO;
                }
            }

            // =========
            // REGISTRAR
            // =========
            else if (accion == "Registrar")
            {
                // Validar que el campo ID permanezca vacío.
                // El ID es generado automáticamente por la base de datos.
                if (usuario.ID_USUARIO != 0)
                {
                    ViewBag.ErrorID =
                        "El ID se genera automáticamente.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que el nombre haya sido capturado.
                if (string.IsNullOrWhiteSpace(usuario.NOMBRE))
                {
                    ViewBag.ErrorNombre =
                        "Debe ingresar el nombre.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que el apellido paterno haya sido capturado.
                if (string.IsNullOrWhiteSpace(usuario.APELLIDO_PATERNO))
                {
                    ViewBag.ErrorApellidoPaterno =
                        "Debe ingresar el apellido paterno.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que el apellido materno haya sido capturado.
                if (string.IsNullOrWhiteSpace(usuario.APELLIDO_MATERNO))
                {
                    ViewBag.ErrorApellidoMaterno =
                        "Debe ingresar el apellido materno.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que la edad se encuentre
                // dentro del rango permitido.
                if (usuario.EDAD < 1 ||
                    usuario.EDAD > 120)
                {
                    ViewBag.ErrorEdad =
                        "La edad debe estar entre 1 y 120 años.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que se haya seleccionado un género.
                if (string.IsNullOrWhiteSpace(usuario.GENERO))
                {
                    ViewBag.ErrorGenero =
                        "Debe seleccionar un género.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que el correo electrónico haya sido capturado.
                if (string.IsNullOrWhiteSpace(usuario.EMAIL))
                {
                    ViewBag.ErrorEmail =
                        "Debe ingresar un correo electrónico.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar el formato del correo electrónico.
                try
                {
                    MailAddress correo =
                        new MailAddress(usuario.EMAIL);
                }
                catch
                {
                    ViewBag.ErrorEmail =
                        "Debe ingresar un correo electrónico válido.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que la confirmación del correo electrónico
                // haya sido capturada.
                if (string.IsNullOrWhiteSpace(usuario.CONFIRMAR_EMAIL))
                {
                    ViewBag.ErrorConfirmarEmail =
                        "Debe confirmar el correo electrónico.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que ambos correos electrónicos coincidan.
                if (usuario.EMAIL != usuario.CONFIRMAR_EMAIL)
                {
                    ViewBag.ErrorConfirmarEmail =
                        "Los correos electrónicos no coinciden.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que la contraseña haya sido capturada.
                if (string.IsNullOrWhiteSpace(usuario.PASSWORD))
                {
                    ViewBag.ErrorPassword =
                        "Debe ingresar una contraseña.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que la confirmación de la contraseña
                // haya sido capturada.
                if (string.IsNullOrWhiteSpace(usuario.CONFIRMAR_PASSWORD))
                {
                    ViewBag.ErrorConfirmarPassword =
                        "Debe confirmar la contraseña.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

                // Validar que ambas contraseñas coincidan.
                if (usuario.PASSWORD != usuario.CONFIRMAR_PASSWORD)
                {
                    ViewBag.ErrorConfirmarPassword =
                        "Las contraseñas no coinciden.";

                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

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
                // Validar que se haya capturado un ID.
                if (usuario.ID_USUARIO == 0)
                {
                    // Limpiar los datos almacenados en el ModelState.
                    ModelState.Clear();

                    ViewBag.ErrorID =
                        "Debe ingresar un ID para realizar la actualización.";

                    // Inicializar nuevamente el formulario.
                    modelo.Usuario =
                        new UsuarioCRUDModel();

                    return View(modelo);
                }

                // Validar que el ID sea mayor que cero.
                if (usuario.ID_USUARIO < 0)
                {
                    // Limpiar los datos almacenados en el ModelState.
                    ModelState.Clear();

                    ViewBag.ErrorID =
                        "El ID debe ser mayor que cero.";

                    // Inicializar nuevamente el formulario.
                    modelo.Usuario =
                        new UsuarioCRUDModel();

                    // Conservar únicamente el ID capturado.
                    modelo.Usuario.ID_USUARIO =
                        usuario.ID_USUARIO;

                    return View(modelo);
                }

                // Verificar que el ID exista en la base de datos.
                UsuarioCRUDModel usuarioExistente =
                    crud.ConsultarUsuarioPorId(
                        usuario.ID_USUARIO);

                if (usuarioExistente == null)
                {
                    // Limpiar los datos almacenados en el ModelState.
                    ModelState.Clear();

                    ViewBag.ErrorID =
                        "No existe un usuario con el ID especificado.";

                    // Inicializar nuevamente el formulario.
                    modelo.Usuario =
                        new UsuarioCRUDModel();

                    // Conservar únicamente el ID capturado.
                    modelo.Usuario.ID_USUARIO =
                        usuario.ID_USUARIO;

                    return View(modelo);
                }

                // Conservar el nombre actual si no fue modificado.
                if (string.IsNullOrWhiteSpace(usuario.NOMBRE))
                {
                    usuario.NOMBRE =
                        usuarioExistente.NOMBRE;
                }

                // Conservar el apellido paterno actual si no fue modificado.
                if (string.IsNullOrWhiteSpace(usuario.APELLIDO_PATERNO))
                {
                    usuario.APELLIDO_PATERNO =
                        usuarioExistente.APELLIDO_PATERNO;
                }

                // Conservar el apellido materno actual si no fue modificado.
                if (string.IsNullOrWhiteSpace(usuario.APELLIDO_MATERNO))
                {
                    usuario.APELLIDO_MATERNO =
                        usuarioExistente.APELLIDO_MATERNO;
                }

                // Conservar la edad actual si no fue modificada.
                if (usuario.EDAD == 0)
                {
                    usuario.EDAD =
                        usuarioExistente.EDAD;
                }

                // Conservar el género actual si no fue modificado.
                if (string.IsNullOrWhiteSpace(usuario.GENERO))
                {
                    usuario.GENERO =
                        usuarioExistente.GENERO;
                }

                // Conservar el correo electrónico actual
                // si no fue modificado.
                if (string.IsNullOrWhiteSpace(usuario.EMAIL))
                {
                    usuario.EMAIL =
                        usuarioExistente.EMAIL;
                }

                // Validar únicamente si el correo electrónico fue modificado.
                if (usuario.EMAIL != usuarioExistente.EMAIL)
                {
                    // Validar el formato del nuevo correo electrónico.
                    try
                    {
                        MailAddress correo =
                            new MailAddress(usuario.EMAIL);
                    }
                    catch
                    {
                        ViewBag.ErrorEmail =
                            "Debe ingresar un correo electrónico válido.";

                        modelo.Usuario =
                            usuario;

                        return View(modelo);
                    }

                    // Validar que se haya capturado la confirmación.
                    if (string.IsNullOrWhiteSpace(usuario.CONFIRMAR_EMAIL))
                    {
                        ViewBag.ErrorConfirmarEmail =
                            "Debe confirmar el correo electrónico.";

                        modelo.Usuario =
                            usuario;

                        return View(modelo);
                    }

                    // Validar que ambos correos coincidan.
                    if (usuario.EMAIL != usuario.CONFIRMAR_EMAIL)
                    {
                        ViewBag.ErrorConfirmarEmail =
                            "Los correos electrónicos no coinciden.";

                        modelo.Usuario =
                            usuario;

                        return View(modelo);
                    }
                }

                // Validar únicamente si se capturó una nueva contraseña.
                if (!string.IsNullOrWhiteSpace(usuario.PASSWORD))
                {
                    // Validar que se haya capturado la confirmación
                    // de la contraseña.
                    if (string.IsNullOrWhiteSpace(usuario.CONFIRMAR_PASSWORD))
                    {
                        ViewBag.ErrorConfirmarPassword =
                            "Debe confirmar la contraseña.";

                        modelo.Usuario =
                            usuario;

                        return View(modelo);
                    }

                    // Validar que ambas contraseñas coincidan.
                    if (usuario.PASSWORD != usuario.CONFIRMAR_PASSWORD)
                    {
                        ViewBag.ErrorConfirmarPassword =
                            "Las contraseñas no coinciden.";

                        modelo.Usuario =
                            usuario;

                        return View(modelo);
                    }
                }

                // Verificar si realmente hubo cambios
                // en la información del usuario.
                bool huboCambios =
                       usuario.NOMBRE != usuarioExistente.NOMBRE
                    || usuario.APELLIDO_PATERNO != usuarioExistente.APELLIDO_PATERNO
                    || usuario.APELLIDO_MATERNO != usuarioExistente.APELLIDO_MATERNO
                    || usuario.EDAD != usuarioExistente.EDAD
                    || usuario.GENERO != usuarioExistente.GENERO
                    || usuario.EMAIL != usuarioExistente.EMAIL
                    || !string.IsNullOrWhiteSpace(usuario.PASSWORD);

                // Verificar si el usuario realizó algún cambio.
                if (!huboCambios)
                {
                    ViewBag.MensajeActualizar =
                        "No se detectaron cambios para actualizar.";

                    // Conservar la información mostrada en el formulario.
                    modelo.Usuario =
                        usuario;

                    return View(modelo);
                }

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

