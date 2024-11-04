using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Solicitud
    {
        [Key]
        public int IdSolicitud { get; set; }

        [ForeignKey("Persona")]
        public int IdPersona { get; set; }
        public Persona? Persona { get; set; }

        [Required]
        [MaxLength(50)]
        public string Carro_Que_Desea { get; set; }

        //Que genere un numero random y que salga aprovado o denegado y impirma todos los datos de esa persona traidos por la cedula
        //es decir si ingreso la cedula me debe impirimir aqui abajo lo de persona, legal,sri y civil de esa persona que esta solicitando el credito

        //No esta creado aun el controllador para esta clase
    }
}
