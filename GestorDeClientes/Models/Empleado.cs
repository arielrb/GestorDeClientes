using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeClientes.Models
{
    public class Empleado
    {
        [Key]
        public int empleadoId { get; set; }
        public string? nombre { get; set; }
        [ForeignKey("Cliente")]
        public int clienteId { get; set; }
        //Instancia del modelo Cliente
        public virtual Cliente clienteAsociado { get; set; }
    }
}
