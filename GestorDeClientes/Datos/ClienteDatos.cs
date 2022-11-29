using GestorDeClientes.Models;
using System.Data;
using System.Data.SqlClient;
namespace GestorDeClientes.Datos
{
    public class ClienteDatos
    {
        /*
        El paso a paso es, 
        BBDD: tiene la info que son los registros
        >> Conexion: Atraves de la conexion (puente/tunel)
        >> Modelos: La info se almacena en Modelos en forma de objeto
        >> Controladores: Reciben el objeto del modelo y lo envian a las vistas
        >> Vistas: muestra la informacion
         
         */
        //Aca pasamos a definir los metodos del CRUD (ABML)

        public List<Cliente> Listar()
        {
            //Esta variable recibe la informacion
            var oLista = new List<Cliente>();
            //Instancia de la conexion
            var conexion = new Conexion();
            //Usando using definimos el tiempo de vida de la conexion
            using (var conexionTemp = new SqlConnection(conexion.getCadenaSQL()))
            {
                //Aca abro la conexion
                conexionTemp.Open();
                //Aca instancio un objeto para las query y la relaciono con el sp
                SqlCommand cmd = new SqlCommand("Listar", conexionTemp);
                cmd.CommandType = CommandType.StoredProcedure;
                //Comienzo la lectura de datos
                using (var lector = cmd.ExecuteReader())
                {
                    //mientras haya registros van a almacenarse en el oLista
                    while (lector.Read())
                    {
                        //Añadiendo por cada vuelta un registro
                        oLista.Add(new Cliente()
                        {
                            Id = Convert.ToInt32(lector["id"]),
                            nombre = Convert.ToString(lector["nombre"]),
                            telefono = Convert.ToString(lector["telefono"]),
                            email = Convert.ToString(lector["email"])
                        });
                    }//Aca ya no existe la variable lector, se destruyo
                }//Aca ya no existe la variable conexionTemp, se destruyo
            }
            return oLista;
        }

        //Metodo Read By, o buscar Cliente
        public Cliente Obtener(int id)
        {
            //Variable que recibe la info de la BBDD
            var oCliente = new Cliente();
            try
            {
                //Instancia de la conexion
                var conexion = new Conexion();

                //Abrimos la conexion y traemos el stored procedure
                using (var conexionTemp = new SqlConnection(conexion.getCadenaSQL()))
                {
                    //Abrimos la conexion temporal
                    conexionTemp.Open();
                    //Traemos el comando SQL
                    SqlCommand cmd = new SqlCommand("Obtener", conexionTemp);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Realizamos la lectura de los registros
                    using (var lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            oCliente.Id = Convert.ToInt32(lector["id"]);
                            oCliente.nombre = Convert.ToString(lector["nombre"]);
                            oCliente.telefono = Convert.ToString(lector["telefono"]);
                            oCliente.email = Convert.ToString(lector["email"]);
                        }
                    }
                }
            }catch(Exception e)
            {
                string error = e.Message;
                return oCliente;
            }
            return oCliente;
        }

        public bool Guardar(Cliente oCliente)
        {
            //Auxiliar para la respuestas
            bool respuesta;
            //Manejamos la excepcion
            try
            {
                //Instaciamos la conexion
                var conexion = new Conexion();
                //Abrimos la conexion temporal
                using (var conexionTemp = new SqlConnection(conexion.getCadenaSQL()))
                {
                    conexionTemp.Open();
                    //Traemos el stored procedure
                    SqlCommand cmd = new SqlCommand("Guardar", conexionTemp);
                    //Agregamos los valores
                    cmd.Parameters.AddWithValue("nombre", oCliente.nombre);
                    cmd.Parameters.AddWithValue("telefono", oCliente.telefono);
                    cmd.Parameters.AddWithValue("email", oCliente.email);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Ejecutamos la no query
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
        public bool Editar(Cliente oCliente)
        {
            //Variable Auxiliar
            bool respuesta;

            //Manejamos la excepcion
            try
            {
                //Instanciamos la coexion
                var conexion = new Conexion();
                //Conectamos con la BBDD temporalmente
                using (var conexionTemp = new SqlConnection(conexion.getCadenaSQL()))
                {
                    //Abrimos la conexion
                    conexionTemp.Open();
                    //Buscamos el stored procedure
                    SqlCommand cmd = new SqlCommand("Editar", conexionTemp);

                    //Este en particular lo va a usar para encontrar el registro
                    cmd.Parameters.AddWithValue("id", oCliente.Id);
                    //Modificamos los valores
                    cmd.Parameters.AddWithValue("nombre", oCliente.nombre);
                    cmd.Parameters.AddWithValue("telefono", oCliente.telefono);
                    cmd.Parameters.AddWithValue("email", oCliente.email);
                    //Ejecutamos la no query
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
        public bool Eliminar(int id)
        {
            //Auxiliar respuesta
            bool respuesta;

            //manejamos la excepcion
            try
            {
                //Instanciamos la conexion
                var conexion = new Conexion();
                //Abrimos la conexion temporal
                using (var conexionTemp = new SqlConnection(conexion.getCadenaSQL()))
                {
                    //Abrimos la conexion
                    conexionTemp.Open();
                    //Buscamos el stored procedure
                    SqlCommand cmd = new SqlCommand("Eliminar", conexionTemp);
                    //Le damos el ID para que los busque
                    cmd.Parameters.AddWithValue("id", id);
                    //Ejecutamos
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}
