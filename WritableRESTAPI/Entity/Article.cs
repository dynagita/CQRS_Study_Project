using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WritableRESTAPI.Entity
{
    public class Article : EntityBase
    {
        [Required]
        [MaxLength(1000, ErrorMessage = "Abstract can't not have more than 1000 words.")]
        public string Abstract { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public User Author { get; set; }

        [Required]
        public int TotalLike { get; set; }

        
        [JsonIgnore]
        [InverseProperty(nameof(Like.Article))]

        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();

    }
}
