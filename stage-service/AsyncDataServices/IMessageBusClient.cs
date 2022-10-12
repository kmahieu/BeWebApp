

using StageService.Dtos;
using StageService.Models;

namespace StageService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void DelStageById(StageUpdatedDto stage);
    }
}