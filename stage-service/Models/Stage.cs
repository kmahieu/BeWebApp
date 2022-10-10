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

        public string? Id { get; set; }
        
    
        public string? name { get; set; }

        public ICollection<Document> document { get; set; } = new List<Document>();


       
    }
}