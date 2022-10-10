

using StageService.Models;

namespace StageService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void SupprPublicationById(Stage Stage);
    }
}