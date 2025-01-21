using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Proyecto.Models
{
    public class Solicitud
    {
        [Key]
        public int IdSolicitud { get; set; }

        [ForeignKey("Persona")]
        public int IdPersona { get; set; }

        [JsonIgnore] // Ignora esta propiedad durante la serialización JSON
        public Persona? Persona { get; set; }

       
    }
}
