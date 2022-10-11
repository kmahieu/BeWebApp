using System;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using DocumentService.Models;
using DocumentService.Dtos;
using DocumentService.Repositories;

namespace PublicationService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        // private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly HttpClient _httpClient;
         private readonly IConfiguration _configuration;

        public EventProcessor(IServiceScopeFactory scopeFactory, HttpClient httpClient, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _httpClient = httpClient;
            // _mapper = mapper;
        }

        public async Task ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                // On souscrit à la méthode UpdateUserById() si la valeur retournée est bien EventType
                case EventType.StageDeleted:
                    await DeleteDocumentByStageId(message);
                    break;
                default:
                    break; 
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            // On déserialise les données pour retourner un objet (texte vers objet json)
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);
            Console.WriteLine($"--> Event Type: {eventType.Event}");

            switch(eventType.Event)
            {
    
                case "Stage_Deleted":
                Console.WriteLine("--> Platform Stage Deleted Event Detected");
                return EventType.StageDeleted;
           
                default:
    
                Console.WriteLine("--> Could not determine the event type");
                return EventType.Undetermined;
            }
        }

        private void DocumentDeleted(string notifcationMessage)
        {
            Console.WriteLine("--> A document has been deleted");
        }

        private async Task<ActionResult> DeleteDocumentByStageId(string DocumentUpdateMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
           
                var repo = scope.ServiceProvider.GetRequiredService<IDocumentRepo>();
                
        
                var stag = JsonSerializer.Deserialize<Stage>(DocumentUpdateMessage);
                Console.WriteLine($"--> Document with Stage Id : {stag.Id} will be deleted"); 

                try
                {
                    var DocumentModelFromRepo = await repo.GetAllDocumentsByStageId(stag.Id);

                    Console.WriteLine(DocumentModelFromRepo);
                    // var PublicationModel = _mapper.Map<UserReadDto>(UserId);
                    foreach(var com in DocumentModelFromRepo)
                    {
                        var DocumentMap = new Document();
                        DocumentMap.Id = com.Id;

                    await repo.DeleteDocument(com.Id);
                    Console.WriteLine("--> Document Deleted");
                        
                    }               

                    if (DocumentModelFromRepo == null)
                    {
                        Console.WriteLine("ERROR : Document not found");
                    }
                
                    
                }

                // Si une erreur survient, on affiche un message
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not modify Publication to DB: {ex.Message}");
                }

                
            }
            return Ok();
        }

 

        private ActionResult Ok()
        {
            throw new NotImplementedException();
        }
    }

    // Type d'Event
    enum EventType
    {
         StageDeleted,
        Undetermined
    }
}