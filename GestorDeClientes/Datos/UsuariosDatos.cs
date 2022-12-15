using GestorDeClientes.Models;
using System.Data;
using System.Data.SqlClient;
namespace GestorDeClientes.Datos
{
    public class UsuariosDatos
    {
        public List<Usuarios> ListarUsuarios()
        {
            //Esta variable recibe la informacion
            var oLista = new List<Usuarios>();
            //Instancia de la conexion
            var conexion = new Conexion();
            //Usando using definimos el tiempo de vida de la conexion
            using (var conexionTemp = new SqlConnection(conexion.getCadenaSQL()))
            {
                //Aca abro la conexion
                conexionTemp.Open();
                //Aca instancio un objeto para las query y la relaciono con el sp
                SqlCommand cmd = new SqlCommand("ListarUsuarios", conexionTemp);
                cmd.CommandType = CommandType.StoredProcedure;
                //Comienzo la lectura de datos
                using (var lector = cmd.ExecuteReader())
                {
                    //mientras haya registros van a almacenarse en el oLista
                    while (lector.Read())
                    {
                        //Añadiendo por cada vuelta un registro
                        oLista.Add(new Usuarios()
                        {
                            UsuarioNombre = Convert.ToString(lector["UsuarioNombre"]),
                            UsuarioContrasenia = Convert.ToString(lector["UsuarioContrasenia"]),
                            UsuarioEmail = Convert.ToString(lector["UsuarioEmail"]),
                            UsuarioRolId = Convert.ToInt32(lector["UsuarioRolId"])
                        });
                    }//Aca ya no existe la variable lector, se destruyo
                }//Aca ya no existe la variable conexionTemp, se destruyo
            }
            return oLista;
        }
        public Usuarios ValidarUsuario(string user, string pass)
        {
            return ListarUsuarios().Where(item => item.UsuarioEmail == user && item.UsuarioContrasenia == pass).FirstOrDefault();
        }
        public Usuarios Autenticar(string user, string pass)
        {
            //Esta variable recibe la informacion
            var usuario = new Usuarios();
            //Instancia de la conexion
            var conexion = new Conexion();
            try
            {
                //Usando using definimos el tiempo de vida de la conexion
                using (var conexionTemp = new SqlConnection(conexion.getCadenaSQL()))
                {
                    //Aca abro la conexion
                    conexionTemp.Open();
                    //Aca instancio un objeto para las query y la relaciono con el sp
                    SqlCommand cmd = new SqlCommand("AutenticarUsuarios", conexionTemp);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Comienzo la lectura de datos
                    using (var lector = cmd.ExecuteReader())
                    {
                        //mientras haya registros van a almacenarse en el oLista
                        while (lector.Read())
                        {
                            usuario.UsuarioNombre = Convert.ToString(lector["UsuarioNombre"]);
                            usuario.UsuarioContrasenia = Convert.ToString(lector["UsuarioContrasenia"]);
                            usuario.UsuarioEmail = Convert.ToString(lector["UsuarioEmail"]);
                            usuario.UsuarioRolId = Convert.ToInt32(lector["UsuarioRolId"]);
                        }
                    }
                }
                return usuario;
            }
            catch
            {
                return null;
            }
        }
    }
}
