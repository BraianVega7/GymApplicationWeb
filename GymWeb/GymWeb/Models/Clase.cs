using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymWeb.Models
{
    public class Clase
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre debe ser obligatorio")]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion debe ser obligatoria")]
        [Display(Name = "Descripcion")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El instructor/a debe ser obligatorio")]
        [Display(Name = "Instructor")]
        public string? Instructor { get; set; }

        [Required(ErrorMessage = "La dificultad debe ser obligatoria")]
        [Display(Name = "Dificultad")]
        public string? Dificultad { get; set; }

    }

}
