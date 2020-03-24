namespace FindMusic.Entity.Helpers
{
    public interface IDbContextFactory
    {
        IDbContextContainer Create();
    }
}
