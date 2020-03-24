using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Interfaces;
using FindMusic.Core.Models;

namespace FindMusic.Core.Repositories
{
    public class AppleMusicRepository : IMusicRepository
    {
        public Task<IReadOnlyCollection<Album>> GetAlbumsByBandNameAsync(string bandName, CancellationToken token)
        {
            return Task.Run<IReadOnlyCollection<Album>>(async () =>
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    client.BaseAddress = new Uri("https://api.music.apple.com/");

                    //var response = await client.GetAsync("v1/catalog/", token);
                    
                    return new List<Album> {new Album {Name = "1"}, new Album {Name = "2"}};
                }
            }, token);
        }
    }
}
