using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using DocumentService.Models;
using DocumentService.Repositories;

namespace DocumentService.Repositories
{
    public class DocumentRepo : IDocumentRepo
    {
        private readonly AppDbContext _context;

        public DocumentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateDocument(Document document)
        {
            if (document != null)
            {
                await _context.document.InsertOneAsync(document);
            }
        }

        public async Task<Document> UpdateDocument(string id, Document document)
        {
            return await _context.document.FindOneAndReplaceAsync(c => c.Id == id,
                new Document {Id = id, name = document.name });
        }

        public async Task<IEnumerable<Document>> GetAllDocument()
        {
            return await _context.document.Find(_ => true).ToListAsync();
        }

        public async Task<Document> GetDocumentById(string id)
        {
            return await _context.document.Find(c => c.Id == id).SingleOrDefaultAsync();
        }


        public async Task DeleteDocument(string id)
        {
           await _context.document.DeleteOneAsync(c => c.Id == id);
        }


    }
}