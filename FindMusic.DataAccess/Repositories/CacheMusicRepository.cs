using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FindMusic.DataAccess.Interfaces;
using FindMusic.DataAccess.Models;
using FindMusic.Entity.Helpers;
using FindMusic.Entity.Models;
using FindMusic.Utils.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FindMusic.DataAccess.Repositories
{
    public class CacheMusicRepository : ICacheMusicRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public CacheMusicRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Task<Result<Status, bool>> IsArtistExistAsync(string artistName, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                using var contextContainer = _dbContextFactory.Create();
                var context = contextContainer.Context;

                try
                {
                    var artistExist = await context.Artists.AsNoTracking().AnyAsync(i => i.Name == artistName, token);
                    return new Result<Status, bool>(Status.Ok, artistExist);
                }
                catch (Exception e)
                {
                    return new Result<Status, bool>(Status.Fail, message: e.Message);
                }

            }, token);
        }

        public Task<Status> AddArtistInfoAsync(FullArtistInfo artistInfo, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                using var contextContainer = _dbContextFactory.Create();
                var context = contextContainer.Context;

                try
                {
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

                    return Status.Ok;
                }
                catch (Exception)
                {
                    return Status.Fail;
                }
            }, token);
        }

        public Task<Result<Status, FullArtistInfo>> GetArtistInfoByNameAsync(string artistName, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                using var contextContainer = _dbContextFactory.Create();
                var context = contextContainer.Context;

                try
                {
                    var artist = await context.Artists
                        .AsNoTracking()

                        .FirstAsync(i => i.Name == artistName, token);

                    var albums = await context.Albums
                        .AsNoTracking()
                        .Where(i => i.ArtistId == artist.Id)
                        .Select(i => new Album {ProviderId = i.ProviderId, Name = i.Name})
                        .ToListAsync(token);

                    return new Result<Status, FullArtistInfo>(Status.Ok, new FullArtistInfo
                    {
                        Artist = new Artist {ProviderId = artist.ProviderId, Name = artist.Name},
                        Albums = albums
                    });
                }
                catch (Exception e)
                {
                    return new Result<Status, FullArtistInfo>(Status.Fail, message: e.Message);
                }
            }, token);
        }
    }
}
