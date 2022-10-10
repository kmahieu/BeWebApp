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

         public StageController(IStageRepo repository, IMapper mapper, HttpClient httpClient, IConfiguration configuration)
         {
             _repository = repository;
             _mapper = mapper;
             _httpClient = httpClient;
             _configuration = configuration;
         }

        [HttpGet]
        public async Task<IEnumerable<Stage>> GetAllContact()
        {
            return await _repository.GetAllStage();
        }

        [HttpGet("{id}", Name = "GetContactById")]
        public async Task<Stage> GetContactById(string id)
        {
            return await _repository.GetStageById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Stage>> CreateTestMessage2([FromBody] Stage Stage)
        {
        

            await _repository.CreateStage(Stage);

            return Ok();
        }

       [HttpPut("{id}", Name = "UpdateStage")]
        public async Task<IActionResult> UpdateStage(string id, [FromBody] Stage Stage)
        {
            
            await _repository.UpdateStage(id, Stage);

            return Ok();
        }

       [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStage(string id)
        {
            await _repository.DeleteStage(id);
            return Ok();
        }
    }
}