using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Proyecto.Models
{
    public class Legal
    {
        [Key]
        public int IdLegal { get; set; }

        [ForeignKey("Persona")]
        public int IdPersona { get; set; }

        [JsonIgnore] // Ignora esta propiedad durante la serialización JSON
        public Persona? Persona { get; set; }

        [Required]
        //"SI" marque el casillero "NO" deje el casillero en blanco
        public Boolean Denuncias { get; set; }

        [Required]
        //"SI" marque el casillero "NO" deje el casillero en blanco
        public Boolean Antecedentes_Penales { get; set; }

        [Required]
        //"SI" marque el casillero "NO" deje el casillero en blanco
        public Boolean Fraudes { get; set; }
    }
}
