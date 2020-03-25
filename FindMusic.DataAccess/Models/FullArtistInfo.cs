using System.Collections.Generic;

namespace FindMusic.DataAccess.Models
{
    public class FullArtistInfo
    {
        public Artist Artist { get; set; }

        public IReadOnlyCollection<Album> Albums { get; set; }
    }
}
