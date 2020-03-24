using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Helpers;
using FindMusic.Core.Models;

namespace FindMusic.Core.Interfaces
{
    public interface IMusicRepository
    {
        Task<Result<Status, FullArtistInfo>> GetAlbumsByBandNameAsync(string bandName, CancellationToken token);
    }
}
