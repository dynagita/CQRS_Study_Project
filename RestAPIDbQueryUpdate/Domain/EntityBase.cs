using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Domain
{
    public class EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]

        public string Id { get; set; }


        [Required]
        [BsonElement("writableRelation")]
        public Int64 WritableRelation { get; set; }
    }
}
