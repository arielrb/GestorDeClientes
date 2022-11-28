using System.Data.SqlClient;
namespace GestorDeClientes.Datos
{
    public class Conexion
    {
        private string cadenaSQL = string.Empty;

        //Constructor de la conexion
        public Conexion()
        {
            //Builder de la conexion
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json").Build();
            //Info de la conexion
            cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;
        }
        //Getter de la cadena de SQL
        public string getCadenaSQL()
            { return cadenaSQL; }
    }
}
