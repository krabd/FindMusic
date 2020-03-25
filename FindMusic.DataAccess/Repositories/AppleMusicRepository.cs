using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FindMusic.DataAccess.Interfaces;
using FindMusic.DataAccess.Models;
using FindMusic.Utils.Helpers;

namespace FindMusic.DataAccess.Repositories
{
    public class AppleMusicRepository : IMusicRepository
    {
        public Task<Result<Status, FullArtistInfo>> GetAlbumsByArtistNameAsync(string artistName, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        //TODO: Apple Music все-таки требует авторизации, для которой нужен аккаунт разработчика, поэтому сделал заглушку,
                        //TODO: Судя по API https://developer.apple.com/documentation/applemusicapi/search_for_catalog_resources, это обычный GET запрос
                        //TODO: с ответом в JSON, написал приблизительный код, как это было бы
                        //var uriBuilder = new UriBuilder("https://api.music.apple.com/v1/catalog/") {Query = $"term='{artistName.Replace(' ', '+')}'&types='albums'"};
                        //var response = await client.GetAsync(uriBuilder.Uri, token);
                        //var result = await response.Content.ReadAsStringAsync();
                        //var albumsInfo = JsonConvert.DeserializeObject<AlbumsResponse>(result);

                        var artist = new Artist { ProviderId = 1, Name = artistName };
                        var albums = new List<Album>
                        {
                            new Album {ProviderId = 1, Name = "1"},
                            new Album {ProviderId = 2, Name = "2"}
                        };

                        return new Result<Status, FullArtistInfo>(Status.Ok, new FullArtistInfo
                        {
                            Artist = artist,
                            Albums = albums
                        });
                    }
                    catch (Exception e)
                    {
                        return new Result<Status, FullArtistInfo>(Status.Fail, message: e.Message);
                    }
                }
            }, token);
        }
    }
}
