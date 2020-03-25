using System.Threading;
using System.Threading.Tasks;
using FindMusic.DataAccess.Models;

namespace FindMusic.Core.Interfaces
{
    public interface IFindMusicService
    {
        Task<FullArtistInfo> GetAlbumsByBandNameAsync(string artistName, CancellationToken token);
    }
}
