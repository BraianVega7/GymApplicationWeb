using GymWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace GymWeb.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
                
        }
        public DbSet<Alumno> Alumno { get; set; }
        public DbSet<Calendario> Calendario { get; set; }
        public DbSet<Clase> Clase { get; set; }
        public DbSet<CondicionPago> CondicionPago { get; set; }
        public DbSet<PrecioMes> PrecioMes { get; set; }
        
    }
}
