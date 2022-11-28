using Microsoft.AspNetCore.Mvc;
using GestorDeClientes.Datos;
namespace GestorDeClientes.Controllers
{
    public class ClienteController : Controller
    {
        //Creamos instancia conexion
        ClienteDatos clienteDatos = new ClienteDatos();
        public IActionResult Index()
        {
            //Traigo la informacion en forma de objeto
            var oLista = clienteDatos.Listar();
            //Le paso el objeto con la informacion a la vista
            return View(oLista);
        }
    }
}
