using System.Threading;
using System.Threading.Tasks;
using FindMusic.DataAccess.Models;
using FindMusic.Utils.Helpers;

namespace FindMusic.DataAccess.Interfaces
{
    public interface IMusicRepository
    {
        Task<Result<Status, FullArtistInfo>> GetAlbumsByArtistNameAsync(string bandName, CancellationToken token);
    }
}
