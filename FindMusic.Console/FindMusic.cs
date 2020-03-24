using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Interfaces;

namespace FindMusic.Console
{
    public class FindMusic
    {
        private readonly IFindMusicService _findMusicService;

        private CancellationTokenSource _cts;

        public FindMusic(IFindMusicService findMusicService)
        {
            _findMusicService = findMusicService;
        }

        public async Task Run()
        {
            while (true)
            {
                _cts?.Cancel();
                _cts = new CancellationTokenSource();
                var token = _cts.Token;

                try
                {
                    var bandName = System.Console.ReadLine();

                    await _findMusicService.GetAlbumsByBandNameAsync(bandName, token);
                }
                catch (Exception e) when(!token.IsCancellationRequested)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
