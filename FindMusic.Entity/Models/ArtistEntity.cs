using System.ComponentModel.DataAnnotations;

namespace FindMusic.Entity.Models
{
    public class ArtistEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long ProviderId { get; set; }

        public string Name { get; set; }
    }
}
