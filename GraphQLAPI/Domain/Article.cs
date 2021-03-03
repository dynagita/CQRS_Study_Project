using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace GraphQLAPI.Domain
{
    public class Article : EntityBase
    {
        [Required]
        [MaxLength(1000, ErrorMessage = "Abstract can't not have more than 1000 words.")]
        [BsonElement("abstract")]
        public string Abstract { get; set; }

        [Required]
        [BsonElement("content")]
        public string Content { get; set; }

        [Required]
        [BsonElement("subject")]
        public string Subject { get; set; }

        [Required]
        [BsonElement("author")]
        public User Author { get; set; }

        [Required]
        [BsonElement("like")]
        public List<Like> Likes { get; set; }

        [Required]
        [BsonElement("totalLike")]
        public int TotalLike { get; set; }
    }
}
