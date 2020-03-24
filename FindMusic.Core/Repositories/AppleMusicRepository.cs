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
                    
                    var response = await client.GetAsync("https://api.music.apple.com/v1/catalog/", token);
                    var result = await response.Content.ReadAsStringAsync();
                    
                    return null;
                }
            }, token);
        }
    }
}
