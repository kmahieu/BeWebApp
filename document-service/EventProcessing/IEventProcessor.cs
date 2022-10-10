using System.Threading.Tasks;

namespace PublicationService.EventProcessing
{
  public interface IEventProcessor
  {
      Task ProcessEvent(string message);
  }
}