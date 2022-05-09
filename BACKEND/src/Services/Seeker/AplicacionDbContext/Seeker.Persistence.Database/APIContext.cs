using API.Seeker.Models;
using Microsoft.EntityFrameworkCore;


namespace API.Seeker.Data
{
    public class APIContext : DbContext
    {
        // conexion BD 
        public APIContext (DbContextOptions<APIContext> options)
            : base(options)
        {
        }

        public DbSet<CaRepository> CaRepository { get; set; }
    }
}
