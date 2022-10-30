using System.Collections.Generic;
using System.Threading.Tasks;
using StageService.Models;

namespace StageService.Repositories
{
    public interface IStageRepo
    {
        Task<IEnumerable<Stage>> GetAllStage();
        Task<Stage> GetStageById(string id);
        Task CreateStage(Stage contact);
        Task<Stage> UpdateStage(string id, Stage contact);

        Task<Stage> UpdateDoc(string id, Document[] stageDocument);
        Task DeleteStage(string id);
    }
}