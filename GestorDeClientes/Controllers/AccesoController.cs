using Microsoft.AspNetCore.Mvc;
using GestorDeClientes.Models;
using GestorDeClientes.Datos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace WebApplication8.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Usuarios _user)
        {
            //Traemos la informacion de la "BBDD"

            UsuariosDatos InfoUsuario = new UsuariosDatos();
            //Validamos la informacion

            var usuario = InfoUsuario.Autenticar(_user.UsuarioNombre, _user.UsuarioContrasenia);

            //Si el usuario existe redirige a home

            if (usuario != null)
            {
                //Claims
                var claims = new List<Claim>
                {

                    new Claim(ClaimTypes.Name, usuario.UsuarioNombre),
                    new Claim("Correo", usuario.UsuarioEmail)
                };

                    claims.Add(new Claim(ClaimTypes.Role, usuario.usuarioRol.detalle));

                    //metemos toda la info en una variable
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    //Metodo primero, controlador despues
                    return RedirectToAction("Index", "Home");
                }
            else
                {
                    TempData["Mensaje"] = "El usuario no existe!";
                    return View();
                }

            }
            //Salir del home de vuelta ala vista login
            public async Task<IActionResult> Salir()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Acceso");
            }
        }
    } 
