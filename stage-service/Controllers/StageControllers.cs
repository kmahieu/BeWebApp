using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Mail;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using StageService.Models;
using StageService.Repositories;
using System.Text;
using StageService.AsyncDataServices;
using StageService.Dtos;

namespace StageService.Controllers
{
    [Route("Stage")]
    [ApiController]
    public class StageController : ControllerBase
    {
         private readonly IStageRepo _repository;
         private readonly IMapper _mapper;
         private readonly HttpClient _httpClient;
         private readonly IConfiguration _configuration;
         private readonly IMessageBusClient _messageBusClient;


         public StageController(IStageRepo repository, IMapper mapper, HttpClient httpClient, IConfiguration configuration, IMessageBusClient messageBusClient)
         {
             _repository = repository;
             _mapper = mapper;
             _httpClient = httpClient;
             _configuration = configuration;
             _messageBusClient = messageBusClient;
         }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stage>>> GetAllStage()
        {
            Console.WriteLine("GetAllDocument");
            var stageItem = await _repository.GetAllStage();

            foreach (var stage in stageItem)
            {

                var documentsItem = await _httpClient.GetAsync($"{_configuration["DocumentService"]}" + "stage/" + stage.Id);
                Console.WriteLine(documentsItem);
                var documentsItemsDeserialize = JsonConvert.DeserializeObject<ICollection<Document>>(
                await documentsItem.Content.ReadAsStringAsync());

                stage.document = documentsItemsDeserialize;
            }

            return Ok(stageItem);
        }


        [HttpGet("{id}", Name = "GetStageById")]
        public async Task<ActionResult<Stage>> GetStageById(string id)
        {

            var stageItem = await _repository.GetStageById(id);

          
                var documentsItem = await _httpClient.GetAsync($"{_configuration["DocumentService"]}" + "stage/" + stageItem.Id);
                Console.WriteLine(documentsItem);
                var documentsItemsDeserialize = JsonConvert.DeserializeObject<ICollection<Document>>(
                await documentsItem.Content.ReadAsStringAsync());

                stageItem.document = documentsItemsDeserialize;
            
            return Ok(stageItem);
        }


        [HttpPost]
        public async Task<IActionResult> CreateStage( [FromBody] StageCreateDto Stage)
        {

            var stageModel = _mapper.Map<Stage>(Stage);

            if(stageModel != null){
            await _repository.CreateStage(stageModel);
            
            var stageReadDto = _mapper.Map<StageReadDto>(stageModel);

            return CreatedAtRoute(nameof(GetStageById), new { Id = stageReadDto.Id }, stageReadDto);
            } else{
                return BadRequest();
            }

        }


       [HttpPut("{id}", Name = "UpdateStage")]
        public async Task<IActionResult> UpdateStage(string id, [FromBody] Stage Stage)
        {

            var stageItem = _repository.GetStageById(id);
            
            if(stageItem == null)
            {
                return NotFound();
            }
            else{
            await _repository.UpdateStage(id, Stage);
            return Ok();
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStage(string id)
        {
            var stageItem = await _repository.GetStageById(id);
            Console.WriteLine("-->"+stageItem.name);

            try
            {
                var stageUpdatedDto = _mapper.Map<StageUpdatedDto>(stageItem);
                Console.WriteLine(stageUpdatedDto.name);

                stageUpdatedDto.Event = "Stage_Deleted";
               Console.WriteLine(stageUpdatedDto.GetType());
               _messageBusClient.DelStageById(stageUpdatedDto);

             }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: Async " + ex.Message);
            }

             if (stageItem != null)
            {
                await _repository.DeleteStage(stageItem.Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}