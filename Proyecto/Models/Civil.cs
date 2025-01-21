using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Proyecto.Models
{
    public class Civil
    {
        [Key]
        public int IdCivil { get; set; }

        [ForeignKey("Persona")]
        public int IdPersona { get; set; }

        [JsonIgnore] // Ignora esta propiedad durante la serialización JSON
        public Persona? Persona { get; set; }

        [Required]
        //"SI" marque el casillero "NO" deje el casillero en blanco
        public Boolean Casado { get; set; }

        [AllowNull]
        [Range(0,10)]
        public int? Hijos { get; set; }

    }
}
