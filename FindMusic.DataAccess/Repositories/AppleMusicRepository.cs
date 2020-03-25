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
        public Task<Result<Status, FullArtistInfo>> GetAlbumsByBandNameAsync(string artistName, CancellationToken token)
        {
            return Task.Run(async () =>
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.Timeout = TimeSpan.FromSeconds(10);
                        client.BaseAddress = new Uri("https://api.music.apple.com/");

                        //var response = await client.GetAsync("v1/catalog/", token);

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
