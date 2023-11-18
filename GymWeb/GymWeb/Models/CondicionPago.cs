using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GymWeb.Models
{
    public class CondicionPago
    {

        [Key]
        [Column("ID")]
        public int Id { get; set; }


        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }


        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRegistro { get; set; }
    }
}
