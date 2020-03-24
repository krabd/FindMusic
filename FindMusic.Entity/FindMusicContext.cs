using FindMusic.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace FindMusic.Entity
{
    public class FindMusicContext : DbContext
    {
        public DbSet<ArtistEntity> Artists { get; set; }

        public DbSet<AlbumEntity> Albums { get; set; }

        public FindMusicContext(DbContextOptions options) : base(options)
        {
        }
    }
}
