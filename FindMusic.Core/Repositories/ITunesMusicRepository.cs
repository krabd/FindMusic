using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Interfaces;
using FindMusic.Core.Models;

namespace FindMusic.Core.Repositories
{
    public class ITunesMusicRepository : IMusicRepository
    {
        public Task<IReadOnlyCollection<Album>> GetAlbumsByBandNameAsync(string bandName, CancellationToken token)
        {
            return Task.Run<IReadOnlyCollection<Album>>(async () =>
            {
                return null;
            });
        }
    }
}
