using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Proyecto.Models
{
    public class Autos
    {
        [Key]
        public int IdAutos { get; set; }

        [ForeignKey("Persona")]
        public int IdPersona { get; set; }

        [JsonIgnore] // Ignora esta propiedad durante la serialización JSON
        public Persona? Persona { get; set; }

        [MaxLength(50, ErrorMessage = "La marca debe de tener como minimo 1 digito")]
        [Required]
        public string Marca { get; set; }

        [MaxLength(50, ErrorMessage = "El modelo debe de tener como minimo 1 digito")]
        [Required]
        public string Modelo { get; set; }

        [NotNull]
        [Range(1990, 2025)]
        public int ano { get; set; }

        [NotNull]
        [Range(0, 99999999)]
        public float precio { get; set; }
    }

}