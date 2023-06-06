using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Test.Models;

public partial class Cliente
{
    public int Id{ get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string Apellido { get; set; } = null!;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    public DateTime FechaNacimiento { get; set; }

    public string? Direccion { get; set; }

    [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono debe tener 10 dígitos.")]
    public long? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "El DNI es obligatorio.")]
    public long Dni { get; set; }

    [RegularExpression(@"^\d{11}$", ErrorMessage = "El número de CUIT debe tener 11 dígitos.")]
    public long? Cuit { get; set; }

    //validations


    public bool IsValidCuit()
    {
        string newCuit = Cuit?.ToString(); // Convertir numero a string
        if (string.IsNullOrEmpty(newCuit) || newCuit.Length != 11)
        {
            return false; 
        }

        newCuit = newCuit.Substring(0, newCuit.Length - 1); // separar digito verificador
        int[] multiplicador = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };

        int sum = 0;
        for (int i = 0; i < newCuit.Length; i++)
        {
            sum += int.Parse(newCuit[i].ToString()) * multiplicador[i];
        }

        int resto = sum % 11;
        int checkDigit = (resto == 0) ? 0 : (11 - resto) % 11;

        int lastDigit = int.Parse(Cuit.Value.ToString()[Cuit.Value.ToString().Length - 1].ToString());

        return checkDigit == lastDigit;
    }


}