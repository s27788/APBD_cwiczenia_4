using Microsoft.EntityFrameworkCore;
using KlinikaAPI.Modele;

namespace KlinikaAPI.Data
{
    public class KlinikaDbContext : DbContext
    {
        public KlinikaDbContext(DbContextOptions<KlinikaDbContext> options) : base(options) { }

        public DbSet<Zwierze> Zwierzeta { get; set; }
        public DbSet<Wizyta> Wizyty { get; set; }
    }
}