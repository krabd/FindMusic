using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Models;

namespace FindMusic.Core.Interfaces
{
    public interface IMusicRepository
    {
        Task<IReadOnlyCollection<Album>> GetAlbumsByBandNameAsync(string bandName, CancellationToken token);
    }
}
