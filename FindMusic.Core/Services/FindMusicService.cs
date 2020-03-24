using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Interfaces;
using FindMusic.Core.Models;

namespace FindMusic.Core.Services
{
    public class FindMusicService : IFindMusicService
    {
        private readonly IMusicRepository _musicRepository;

        public FindMusicService(IMusicRepository musicRepository)
        {
            _musicRepository = musicRepository;
        }

        public async Task<IReadOnlyCollection<Album>> GetAlbumsByBandNameAsync(string bandName, CancellationToken token)
        {
            return await _musicRepository.GetAlbumsByBandNameAsync(bandName, token);
        }
    }
}
