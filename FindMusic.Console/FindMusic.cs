using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FindMusic.Core.Interfaces;
using FindMusic.Utils.Helpers;

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
                    if (artistInfo.Value == Status.Fail)
                    {
                        System.Console.WriteLine($"Error: {artistInfo.Message}");
                    }
                    else
                    {
                        foreach (var album in artistInfo.Model.Albums)
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
