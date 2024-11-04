using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Models
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "El número de Cédula debe tener 10 dígitos.")]
        [MaxLength(10, ErrorMessage = "El número de Cédula debe tener 10 dígitos.")]
        public string Cedula { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [Range(0, 100)]
        public int Edad { get; set; }

        [Required]
        public DateTime Fecha_Nacimiento { get; set; }

        [Required]
        [RegularExpression("^(Masculino|Femenino)$", ErrorMessage = "El género debe ser 'Masculino' o 'Femenino'.")]
        public string Genero { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "El número de teléfono debe tener 10 dígitos y comenzar con 0.")]
        [MaxLength(10, ErrorMessage = "El número de teléfono debe tener 10 dígitos y comenzar con 0.")]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }
    }
}


