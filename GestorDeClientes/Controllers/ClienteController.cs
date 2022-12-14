using Microsoft.AspNetCore.Mvc;
using GestorDeClientes.Datos;
using GestorDeClientes.Models;

namespace GestorDeClientes.Controllers
{
    [Route("mvc/[controller]")]
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
        //Metodo para la vista
        public IActionResult Guardar()
        {
            return View();
        }
        //Metodo para la logica, para guardar
        [HttpPost]
        public IActionResult Guardar(Cliente oCliente)
        {
            var respuesta = clienteDatos.Guardar(oCliente);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        //Metodo para la vista
        public IActionResult Editar(int id)
        {
            //Este metodo devuelve la vista segun el ID
            var oCliente = clienteDatos.Obtener(id);
            return View();
        }

        //Metodo para la logica, para modificar
        [HttpPost]
        public IActionResult Editar(Cliente oCliente)
        {
            var respuesta = clienteDatos.Editar(oCliente);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //Metodo para la vista para eliminar
        public IActionResult Eliminar(int id)
        {
            var oCliente = clienteDatos.Obtener(id);
            return View(oCliente);
        }

        //Metodo para la logica de eliminar el registro
        [HttpPost]
        public IActionResult Eliminar(Cliente oCliente)
        {
            var respuesta = clienteDatos.Eliminar(oCliente.Id);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
