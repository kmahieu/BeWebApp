using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StageService.Models
{
    public class Stage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
<<<<<<< HEAD

        public string? Event { get; set; }
        public string name { get; set; }
        public ICollection<Document>? document { get; set; } = new List<Document>();
=======
        public string? name { get; set; }

        public ICollection<Document> document { get; set; } = new List<Document>();
>>>>>>> 647aef85afe110dd1b635779db392090db0362d2

    }
}
