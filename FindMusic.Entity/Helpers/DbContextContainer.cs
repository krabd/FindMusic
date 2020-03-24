using System;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.Entity.Helpers
{
    internal class DbContextContainer : IDbContextContainer
    {
        private readonly IServiceScope _scope;

        public FindMusicContext Context { get; }

        internal DbContextContainer(IServiceScope scope)
        {
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Context = scope.ServiceProvider.GetService<FindMusicContext>();
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}
