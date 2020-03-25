using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using FindMusic.Core.Interfaces;
using FindMusic.Utils.Helpers;
using FindMusic.WPF.Helpers;
using Prism.Commands;

namespace FindMusic.WPF.ViewModels
{
    public class FindMusicViewModel : ViewModelBase
    {
        private readonly IFindMusicService _findMusicService;

        private CancellationTokenSource _cts;
        private string _artistName;

        public ObservableCollection<string> Albums { get; } = new ObservableCollection<string>();

        public string ArtistName
        {
            get => _artistName;
            set => SetProperty(ref _artistName, value);
        }

        public ICommand SearchCommand { get; }

        public FindMusicViewModel(IFindMusicService findMusicService)
        {
            _findMusicService = findMusicService;

            SearchCommand = new DelegateCommand(OnSearch);
        }

        private async void OnSearch()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Tools.DispatchedInvoke(() => Albums.Clear());

            try
            {
                var artistName = ArtistName;

                var artistInfo = await _findMusicService.GetAlbumsByArtistNameAsync(artistName, token);
                if (artistInfo.Value == Status.Fail)
                {
                    Tools.DispatchedInvoke(() => Albums.Add($"Error: {artistInfo.Message}"));
                }
                else
                {
                    Tools.DispatchedInvoke(() =>
                        {
                            foreach (var album in artistInfo.Model.Albums)
                            {
                                Albums.Add(album.Name);
                            }
                        }
                    );
                }
            }
            catch (Exception e) when (!token.IsCancellationRequested)
            {
                Debug.WriteLine(e);
            }
        }
    }
}