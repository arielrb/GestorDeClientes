using GestorDeClientes.Datos;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeClientes.Controllers
{
    public class EmpleadoController : Controller
    {
        //Creamos instancia conexion
        EmpleadoDatos empDatos = new EmpleadoDatos();
        public IActionResult Index()
        {
            //Traigo la informacion en forma de objeto
            var oLista = empDatos.Listar();
            //Le paso el objeto con la informacion a la vista
            return View(oLista);
        }
    }
}
