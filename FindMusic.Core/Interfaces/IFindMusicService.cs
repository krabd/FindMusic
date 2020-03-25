using System.Threading;
using System.Threading.Tasks;
using FindMusic.DataAccess.Models;

namespace FindMusic.Core.Interfaces
{
    public interface IFindMusicService
    {
        Task<FullArtistInfo> GetAlbumsByArtistNameAsync(string artistName, CancellationToken token);
    }
}
