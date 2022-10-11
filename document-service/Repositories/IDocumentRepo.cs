using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentService.Models;

namespace DocumentService.Repositories
{
    public interface IDocumentRepo
    {
        Task<IEnumerable<Document>> GetAllDocument();
        Task<IEnumerable<Document>> GetAllDocumentsByStageId(string stageId);

        Task<Document> GetDocumentById(string id);
        Task CreateDocument(Document contact);
        Task<Document> UpdateDocument(string id, Document contact);
        Task DeleteDocument(string id);
    }
}