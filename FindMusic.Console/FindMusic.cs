using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Interfaces;

namespace FindMusic.Console
{
    public class FindMusic
    {
        private readonly CancellationTokenSource _mainCts = new CancellationTokenSource();
        private readonly IFindMusicService _findMusicService;

        private CancellationTokenSource _cts;

        public FindMusic(IFindMusicService findMusicService)
        {
            _findMusicService = findMusicService;
        }

        public async Task Run()
        {
            while (!_mainCts.IsCancellationRequested)
            {
                _cts?.Cancel();
                _cts = new CancellationTokenSource();
                var token = _cts.Token;

                try
                {
                    var artistName = System.Console.ReadLine();

                    var artistInfo = await _findMusicService.GetAlbumsByArtistNameAsync(artistName, token);
                    if (artistInfo == null)
                    {
                        System.Console.WriteLine("Artist not found");
                    }
                    else
                    {
                        foreach (var album in artistInfo.Albums)
                        {
                            System.Console.WriteLine(album.Name);
                        }
                    }
                }
                catch (Exception e) when(!token.IsCancellationRequested)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        public void Stop()
        {
            _mainCts.Cancel();
        }
    }
}
