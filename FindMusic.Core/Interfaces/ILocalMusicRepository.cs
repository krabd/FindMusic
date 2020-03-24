using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Models;

namespace FindMusic.Core.Interfaces
{
    public interface ILocalMusicRepository
    {
        Task<bool> IsArtistExistAsync(string artistName, CancellationToken token);

        Task AddArtistInfoAsync(FullArtistInfo artistInfo, CancellationToken token);

        Task<FullArtistInfo> GetArtistInfoByNameAsync(string artistName, CancellationToken token);
    }
}
