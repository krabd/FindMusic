using FindMusic.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FindMusic.WPF
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FindMusicContext>
    {
        public FindMusicContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FindMusicContext>();
            var connectionString = "Data Source=FindMusicDatabase.db";
            builder.UseSqlite(connectionString);
            return new FindMusicContext(builder.Options);
        }
    }
}