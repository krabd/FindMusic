using System.Threading;
using System.Threading.Tasks;
using FindMusic.DataAccess.Models;
using FindMusic.Utils.Helpers;

namespace FindMusic.Core.Interfaces
{
    public interface IFindMusicService
    {
        Task<Result<Status, FullArtistInfo>> GetAlbumsByArtistNameAsync(string artistName, CancellationToken token);
    }
}
