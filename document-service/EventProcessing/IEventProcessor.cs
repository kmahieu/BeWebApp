using System.Threading.Tasks;

namespace DocumentService.EventProcessing
{
  public interface IEventProcessor
  {
      Task ProcessEvent(string message);
  }
}