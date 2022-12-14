using GestorDeClientes.Models;
using System.Data;
using System.Data.SqlClient;
namespace GestorDeClientes.Datos
{
    public class EmpleadoDatos
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

        public List<Empleado> Listar()
        {
            //Esta variable recibe la informacion
            var oLista = new List<Empleado>();
            //Instancia de la conexion
            var conexion = new Conexion();
            //Usando using definimos el tiempo de vida de la conexion
            using (var conexionTemp = new SqlConnection(conexion.getCadenaSQL()))
            {
                //Aca abro la conexion
                conexionTemp.Open();
                //Aca instancio un objeto para las query y la relaciono con el sp
                SqlCommand cmd = new SqlCommand("empleadoListar", conexionTemp);
                cmd.CommandType = CommandType.StoredProcedure;
                //Comienzo la lectura de datos
                using (var lector = cmd.ExecuteReader())
                {
                    //mientras haya registros van a almacenarse en el oLista
                    while (lector.Read())
                    {
                        //Añadiendo por cada vuelta un registro
                        oLista.Add(new Empleado()
                        {
                            empleadoId = Convert.ToInt32(lector["empleadoId"]),
                            nombre = Convert.ToString(lector["nombre"]),
                            clienteId = Convert.ToInt32(lector["clienteId"]),
                            clienteAsociado = new Cliente()
                            {
                                Id = Convert.ToInt32(lector["id"]),
                                nombre = Convert.ToString(lector["nombre"]),
                                telefono = Convert.ToString(lector["telefono"]),
                                email = Convert.ToString(lector["email"])
                            }
                        });
                    }//Aca ya no existe la variable lector, se destruyo
                }//Aca ya no existe la variable conexionTemp, se destruyo
            }
            Console.WriteLine(oLista[0]);
            return oLista;
        }

        //Metodo Read By, o buscar Cliente
        public Empleado Obtener(int idContacto)
        {
            //Variable que recibe la info de la BBDD
            var oEmpleado = new Empleado();
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
                    cmd.Parameters.AddWithValue("IdContacto", idContacto);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Realizamos la lectura de los registros
                    using (var lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            oEmpleado.empleadoId = Convert.ToInt32(lector["empleadoId"]);
                            oEmpleado.nombre = Convert.ToString(lector["nombre"]);
                            oEmpleado.clienteId = Convert.ToInt32(lector["clienteId"]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return oEmpleado;
            }
            return oEmpleado;
        }
    }
}
