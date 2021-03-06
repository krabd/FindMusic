﻿using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Interfaces;
using FindMusic.DataAccess.Interfaces;
using FindMusic.DataAccess.Models;
using FindMusic.Utils.Helpers;

namespace FindMusic.Core.Services
{
    //TODO: На уровне всего проекта принимается за данность, что имена у всех исполнителей уникальные.
    //TODO: Это допущение необходимо, так как не понятно как идентифицировать исполнителя в локальном кеше, если я не получаю ответа от провайдера музыки.
    public class FindMusicService : IFindMusicService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly ICacheMusicRepository _cacheMusicRepository;

        public FindMusicService(IMusicRepository musicRepository, ICacheMusicRepository cacheMusicRepository)
        {
            _musicRepository = musicRepository;
            _cacheMusicRepository = cacheMusicRepository;
        }

        public async Task<Result<Status, FullArtistInfo>> GetAlbumsByArtistNameAsync(string artistName, CancellationToken token)
        {
            var albumsResultTask = _musicRepository.GetAlbumsByArtistNameAsync(artistName, token);
            var localArtistInfoExistTask = _cacheMusicRepository.IsArtistExistAsync(artistName, token);
            await Task.WhenAll(albumsResultTask, localArtistInfoExistTask);

            var albumsResult = albumsResultTask.Result;
            var localArtistInfoExist = localArtistInfoExistTask.Result;

            switch (albumsResult.Value)
            {
                case Status.Ok:

                    if (localArtistInfoExist.Value == Status.Ok && !localArtistInfoExist.Model)
                        await _cacheMusicRepository.AddArtistInfoAsync(albumsResult.Model, token);

                    return new Result<Status, FullArtistInfo>(Status.Ok, albumsResult.Model);

                case Status.Fail:

                    if (localArtistInfoExist.Value == Status.Ok && localArtistInfoExist.Model)
                        return await _cacheMusicRepository.GetArtistInfoByNameAsync(artistName, token);
                    
                    break;
            }

            return new Result<Status, FullArtistInfo>(Status.Fail);
        }
    }
}
