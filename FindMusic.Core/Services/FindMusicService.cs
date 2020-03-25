using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Interfaces;
using FindMusic.DataAccess.Interfaces;
using FindMusic.DataAccess.Models;
using FindMusic.Utils.Helpers;

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
            var albumsResultTask = _musicRepository.GetAlbumsByBandNameAsync(artistName, token);
            var localArtistInfoExistTask = _localMusicRepository.IsArtistExistAsync(artistName, token);
            await Task.WhenAll(albumsResultTask, localArtistInfoExistTask);

            var albumsResult = albumsResultTask.Result;
            var localArtistInfoExist = localArtistInfoExistTask.Result;

            switch (albumsResult.Value)
            {
                case Status.Ok:

                    if (!localArtistInfoExist)
                        await _localMusicRepository.AddArtistInfoAsync(albumsResult.Model, token);

                    return albumsResult.Model;

                case Status.Fail:

                    return localArtistInfoExist ? await _localMusicRepository.GetArtistInfoByNameAsync(artistName, token) : null;
            }

            return null;
        }
    }
}
