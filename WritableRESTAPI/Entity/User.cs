using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WritableRESTAPI.Entity
{
    public class User : EntityBase
    {
        [Required]
        [MaxLength(100, ErrorMessage = "You can't inform more than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "You can't inform more than 100 characters.")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "You can't inform more than 100 characters.")]
        public string Email { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Like.User))]

        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();

        [JsonIgnore]
        [InverseProperty(nameof(Article.Author))]
        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
    }
}
