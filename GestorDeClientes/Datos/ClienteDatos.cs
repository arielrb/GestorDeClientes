using GestorDeClientes.Models;
using System.Data.SqlClient;
using System.Data;
namespace GestorDeClientes.Datos
{
    public class ClienteDatos
    {
        /*
         El paso a paso es, 
        BBDD: tiene la info
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
            using( var conexionTemp = new SqlConnection(conexion.getCadenaSQL()))
            {
                //Aca abro la conexion
                conexionTemp.Open();
                //Aca instancio un objeto para las query y la relaciono con el sp
                SqlCommand cmd = new SqlCommand("Listar",conexionTemp);
                cmd.CommandType = CommandType.StoredProcedure;
                //Comienzo la lectura de datos
                using(var lector = cmd.ExecuteReader())
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
    }
}
