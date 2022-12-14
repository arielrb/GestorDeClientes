﻿using System.ComponentModel.DataAnnotations;

namespace GestorDeClientes.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string? nombre { get; set; }
        public string? telefono { get; set; }
        public string? email { get; set; }

    }
}
