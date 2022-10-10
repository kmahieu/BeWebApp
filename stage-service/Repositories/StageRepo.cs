using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using StageService.Models;

namespace StageService.Repositories
{
    public class StageRepo : IStageRepo
    {
        private readonly AppDbContext _context;

        public StageRepo(AppDbContext context)
        {

            _context = context;

        }

        public async Task CreateStage(Stage contact)
        {
            if (contact != null)
            {
                await _context.Stage.InsertOneAsync(contact);
            }
        }

        public async Task<Stage> UpdateStage(string id, Stage Stage)
        {
            return await _context.Stage.FindOneAndReplaceAsync(c => c.Id == id,
                new Stage {Id = id, name = Stage.name });
        }

        public async Task<IEnumerable<Stage>> GetAllStage()
        {
            return await _context.Stage.Find(_ => true).ToListAsync();
        }

        public async Task<Stage> GetStageById(string id)
        {
            return await _context.Stage.Find(c => c.Id == id).SingleOrDefaultAsync();
        }


        public async Task DeleteStage(string id)
        {
           await _context.Stage.DeleteOneAsync(c => c.Id == id);
        }


    }
}