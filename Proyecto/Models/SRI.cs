using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class SRI
    {
        [Key]
        public int IdSRI{ get; set; }

        [ForeignKey("Persona")]
        public int IdPersona { get; set; }
        public Persona? Persona { get; set; }

        [Required]
        [MaxLength(50)]
        //Ingrese su empleo
        public string Trabajo { get; set; }

        [Required]
        [Range(0, 1000000)]
        public int Ingresos_Mensuales { get; set; }
        
        [Required]
        //"SI" marque el casillero "NO" deje el casillero en blanco
        public Boolean Deudas_Activas { get; set; }
        
        [Required]
        [MaxLength(1000000000)]
        //Ingrese Todos sus Bienes separados por una ","
        public string Bienes { get; set; }
    }
}
