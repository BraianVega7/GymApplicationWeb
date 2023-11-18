using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymWeb.Models
{
    public class Calendario
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Display(Name = "Clase")]
        public int ClaseRefId { get; set; }
        [ForeignKey("ClaseRefId")]
        public virtual Clase? Clases { get; set; }

        [Display(Name = "Dia")]
        public string? Dia { get; set; }

        [Display(Name = "HoraInicio")]
        public DateTime HoraInicio { get; set; }

        [Display(Name = "HoraFin")]
        public DateTime HoraFin { get; set; }
    }
}
