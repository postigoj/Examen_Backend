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

    public IEnumerable<ValidationResult> GetValidationResults()
    {
        var validationContext = new ValidationContext(this, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();

        Validator.TryValidateObject(this, validationContext, validationResults, validateAllProperties: true);

        return validationResults;
    }



    public bool IsValidCuit()
    {
        string cuitWithoutDigit = Cuit?.ToString(); // Convert nullable long to string
        if (string.IsNullOrEmpty(cuitWithoutDigit) || cuitWithoutDigit.Length != 11)
        {
            return false; 
        }

        cuitWithoutDigit = cuitWithoutDigit.Substring(0, cuitWithoutDigit.Length - 1); // Remove the check digit
        int[] multiplier = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };

        int sum = 0;
        for (int i = 0; i < cuitWithoutDigit.Length; i++)
        {
            sum += int.Parse(cuitWithoutDigit[i].ToString()) * multiplier[i];
        }

        int remainder = sum % 11;
        int checkDigit = (remainder == 0) ? 0 : (11 - remainder) % 11;

        int lastDigit = int.Parse(Cuit.Value.ToString()[Cuit.Value.ToString().Length - 1].ToString());

        return checkDigit == lastDigit;
    }


}