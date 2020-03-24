using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Helpers;
using FindMusic.Core.Interfaces;
using FindMusic.Core.Models;

namespace FindMusic.Core.Services
{
    public class FindMusicService : IFindMusicService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly ILocalMusicRepository _localMusicRepository;

        public FindMusicService(IMusicRepository musicRepository, ILocalMusicRepository localMusicRepository)
        {
            _musicRepository = musicRepository;
            _localMusicRepository = localMusicRepository;
        }

        public async Task<FullArtistInfo> GetAlbumsByBandNameAsync(string artistName, CancellationToken token)
        {
            var albumsResult = await _musicRepository.GetAlbumsByBandNameAsync(artistName, token);

            switch (albumsResult.Value)
            {
                case Status.Ok:

                    var localArtistInfoExist = await _localMusicRepository.IsArtistExistAsync(artistName, token);

                    if (localArtistInfoExist)
                        await _localMusicRepository.AddArtistInfoAsync(albumsResult.Model, token);

                    return albumsResult.Model;

                case Status.Fail:

                    var localArtistInfo = await _localMusicRepository.GetArtistInfoByNameAsync(artistName, token);
                    return localArtistInfo;
            }

            return null;
        }
    }
}
