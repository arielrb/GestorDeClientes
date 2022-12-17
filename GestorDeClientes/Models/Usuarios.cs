namespace GestorDeClientes.Models
{
    public class Usuarios
    {
        public string? UsuarioNombre { get; set; }
        public string? UsuarioContrasenia { get; set; }
        public string? UsuarioEmail { get; set; }
        //El siguiente seria la foreign key
        public int UsuarioRolId { get; set; }
        //La siguiente seria el objeto rol que tiene el detalle
        public Rol usuarioRol { get; set; }

    }
}
