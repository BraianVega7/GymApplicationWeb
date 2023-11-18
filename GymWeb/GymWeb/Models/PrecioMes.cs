using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymWeb.Models
{
    public class PrecioMes
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Display(Name = "Clase")]
        public int ClaseRefId { get; set; }
        [ForeignKey("ClaseRefId")]
        public virtual Clase? Clases { get; set; }

        [Display(Name ="Precio")]
        public int Cuota {  get; set; }

    }
}
