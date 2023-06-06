using System.ComponentModel.DataAnnotations;

namespace Api.Test.Models;

public partial class UsuarioAcceso
{
    public short Id { get; set; }

    [Required]
    [MaxLength(10, ErrorMessage = "La contraseña debe tener una longitud mínima de 8 caracteres.")] 
    public string Usuario { get; set; } = null!;

    [Required]
    [MaxLength(10, ErrorMessage = "La contraseña debe tener una longitud mínima de 8 caracteres.")]
    public string Password { get; set; } = null!;
}


