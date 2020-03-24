namespace FindMusic.Context
{
    public class FindMusicContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=sqlitedemo.db");
    }
}
