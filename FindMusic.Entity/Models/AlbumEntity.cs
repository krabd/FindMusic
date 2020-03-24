using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindMusic.Entity.Models
{
    public class AlbumEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long ProviderId { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Artist))]
        public long ArtistId { get; set; }

        #region Navigation Properties

        public ArtistEntity Artist { get; set; }

        #endregion
    }
}
