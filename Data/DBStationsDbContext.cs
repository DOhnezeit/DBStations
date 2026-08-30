using DBStations.Models;
using Microsoft.EntityFrameworkCore;

namespace DBStations.Data
{
    public class DBStationsDbContext( DbContextOptions<DBStationsDbContext> options ) : DbContext(options)
    {
        public DbSet<Station> Stations => Set<Station>();
    }
}
