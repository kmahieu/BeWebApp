using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StageService.Models;

namespace StageService.Dtos
{
    public class StageReadDto
    {
        public string Id { get; set; }
        public string? name { get; set; }
        public ICollection<Document> document { get; set; } = new List<Document>();

    }
}