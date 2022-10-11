

using StageService.Models;

namespace StageService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void DelStageById(Stage Stage);
    }
}