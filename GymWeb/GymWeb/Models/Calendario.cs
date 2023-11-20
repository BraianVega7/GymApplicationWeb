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

        [Required(ErrorMessage = "El dia debe ser obligatorio")]
        [Display(Name = "Dia")]
        public string? Dia { get; set; }

        [Required(ErrorMessage = "La hora de inicio debe ser obligatorio")]
        [Display(Name = "HoraInicio")]
        [Column(TypeName = "time")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora final debe ser obligatorio")]
        [Display(Name = "HoraFin")]
        [Column(TypeName = "time")]
        public TimeSpan HoraFin { get; set; }
    }
}
