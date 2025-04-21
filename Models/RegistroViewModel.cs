using System.ComponentModel.DataAnnotations;

namespace CyH_Techno_Store.Models;

public class RegistroViewModel
{
    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    [MaxLength(100, ErrorMessage = "No puede ser mayor a 100 caracteres")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MaxLength(100, ErrorMessage = "No puede ser mayor a 100 caracteres")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "La confirmación de contraseña es obligatoria")]
    [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden")]
    public string? ConfirmPassword { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
    [StringLength(256)]
    public string? Correo { get; set; }

    [StringLength(25, MinimumLength = 8, ErrorMessage = "El teléfono debe tener entre 8 y 25 dígitos")]
    public string? Telefono { get; set; }

    [StringLength(256, MinimumLength = 6, ErrorMessage = "La dirección debe tener entre 6 y 256 caracteres")]
    public string? Direccion { get; set; }

    [StringLength(19, ErrorMessage = "El número de tarjeta no puede exceder 19 caracteres")]
    public string? NumeroTarjeta { get; set; }
}