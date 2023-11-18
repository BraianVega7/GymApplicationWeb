using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymWeb.Models
{
    public class Alumno
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [Display(Name = "Apellido")]
        public string? Apellido { get; set; }

        [Display(Name = "Dni")]
        public int Dni { get; set; }

        [Display(Name = "Edad")]
        public int Edad { get; set; }

        [Display(Name = "Telefono")]
        public int Telefono {  get; set; }

        [Display(Name = "Direccion")]
        public string? Direccion { get; set; }

        [Display(Name = "Clase")]
        public int ClaseRefId { get; set; }
        [ForeignKey("ClaseRefId")]
        public virtual Clase? Clases { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }

    }
}
