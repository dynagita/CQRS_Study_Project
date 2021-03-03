using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Domain
{
    public class User : EntityBase
    {                

        [Required]
        [MaxLength(100, ErrorMessage = "You can't set more than 100 characters.")]
        [BsonElement("name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "You can't set more than 100 characters.")]
        [BsonElement("lastName")]
        public string LastName { get; set; }


        [Required]
        [MaxLength(100, ErrorMessage = "You can't set more than 100 characters.")]
        [BsonElement("email")]
        public string Email { get; set; }
    }
}
