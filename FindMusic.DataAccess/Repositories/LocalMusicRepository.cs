using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FindMusic.DataAccess.Interfaces;
using FindMusic.DataAccess.Models;
using FindMusic.Entity.Helpers;
using FindMusic.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace FindMusic.DataAccess.Repositories
{
    public class LocalMusicRepository : ILocalMusicRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public LocalMusicRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Task<bool> IsArtistExistAsync(string artistName, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                using var contextContainer = _dbContextFactory.Create();
                var context = contextContainer.Context;

                return await context.Artists.AsNoTracking().AnyAsync(i => i.Name == artistName, token);
            }, token);
        }

        public Task AddArtistInfoAsync(FullArtistInfo artistInfo, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                using var contextContainer = _dbContextFactory.Create();
                var context = contextContainer.Context;

                var artist = new ArtistEntity
                {
                    ProviderId = artistInfo.Artist.ProviderId,
                    Name = artistInfo.Artist.Name
                };

                context.Artists.Add(artist);
                await context.SaveChangesAsync(token);

                context.Albums.AddRange(artistInfo.Albums.Select(i => new AlbumEntity
                {
                    ProviderId = i.ProviderId,
                    Name = i.Name,
                    ArtistId = artist.Id
                }));
                await context.SaveChangesAsync(token);
            }, token);
        }

        public Task<FullArtistInfo> GetArtistInfoByNameAsync(string artistName, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                using var contextContainer = _dbContextFactory.Create();
                var context = contextContainer.Context;

                var artist = await context.Artists
                    .AsNoTracking()
                    
                    .FirstAsync(i => i.Name == artistName, token);

                var albums = await context.Albums
                    .AsNoTracking()
                    .Where(i => i.ArtistId == artist.Id)
                    .Select(i => new Album {ProviderId = i.ProviderId, Name = i.Name})
                    .ToListAsync(token);

                return new FullArtistInfo
                {
                    Artist = new Artist {ProviderId = artist.ProviderId, Name = artist.Name},
                    Albums = albums
                };
            }, token);
        }
    }
}
