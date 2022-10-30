using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using StageService.Models;
using StageService.Dtos;

namespace StageService.Repositories
{
    public class StageRepo : IStageRepo
    {
        private readonly AppDbContext _context;

        public StageRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateStage(Stage stage)
        {
            if (stage != null)
            {
                await _context.stage.InsertOneAsync(stage);
            }
        }

        public async Task<Stage> UpdateStage(string id, Stage Stage)
        {
            return await _context.stage.FindOneAndReplaceAsync(c => c.Id == id,
                new Stage {Id = id, name = Stage.name });
        }

        public async Task<IEnumerable<Stage>> GetAllStage()
        {
            return await _context.stage.Find(_ => true).ToListAsync();
        }

        public async Task<Stage> UpdateDoc(string id, Document[] stageDocument)
        {
            return await _context.stage.FindOneAndUpdateAsync(
                Builders<Stage>.Filter.Where(u => u.Id == id), 
                Builders<Stage>.Update.Set(u => u.document, stageDocument));
        }

        public async Task<Stage> GetStageById(string id)
        {
            return await _context.stage.Find(c => c.Id == id).SingleOrDefaultAsync();
        }


        public async Task DeleteStage(string id)
        {
           await _context.stage.DeleteOneAsync(c => c.Id == id);
        }


    }
}