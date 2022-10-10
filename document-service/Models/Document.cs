using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentService.Models
{
    public class Document
    {
        [BsonId]
    
        public string? Id { get; set; }
        
        
        public string? name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? stage_id { get; set; }
       
    }
}