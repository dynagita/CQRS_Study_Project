using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;


namespace GraphQLAPI.Domain
{
    public class Like : EntityBase
    {
        [Required]
        [BsonElement("userId")]
        public string UserId { get; set; }

        [Required]
        [BsonElement("articleId")]
        public string ArticleId { get; set; }
    }
}
