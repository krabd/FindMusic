using System.Threading;
using System.Threading.Tasks;
using FindMusic.DataAccess.Models;
using FindMusic.Utils.Helpers;

namespace FindMusic.DataAccess.Interfaces
{
    public interface ICacheMusicRepository
    {
        Task<Result<Status, bool>> IsArtistExistAsync(string artistName, CancellationToken token);

        Task<Status> AddArtistInfoAsync(FullArtistInfo artistInfo, CancellationToken token);

        Task<Result<Status, FullArtistInfo>> GetArtistInfoByNameAsync(string artistName, CancellationToken token);
    }
}
