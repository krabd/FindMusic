using System;
using Microsoft.Extensions.DependencyInjection;

namespace FindMusic.Entity.Helpers
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDbContextContainer Create()
        {
            var scope = _serviceProvider.CreateScope();
            return new DbContextContainer(scope);
        }
    }
}
