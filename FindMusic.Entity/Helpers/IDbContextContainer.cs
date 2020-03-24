using System;

namespace FindMusic.Entity.Helpers
{
    public interface IDbContextContainer : IDisposable
    {
        FindMusicContext Context { get; }
    }
}
