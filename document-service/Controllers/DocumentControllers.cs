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
using DocumentService.Repositories;
using DocumentService.Models;

namespace DocumentService.Controllers
{
    [Route("document")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
         private readonly IDocumentRepo _repository;
         private readonly IMapper _mapper;
         private readonly HttpClient _httpClient;
         private readonly IConfiguration _configuration;

         public DocumentController(IDocumentRepo repository, IMapper mapper, HttpClient httpClient, IConfiguration configuration)
         {
             _repository = repository;
             _mapper = mapper;
             _httpClient = httpClient;
             _configuration = configuration;
         }

        [HttpGet]
        public async Task<IEnumerable<Document>> GetAllDocument()
        {
            return await _repository.GetAllDocument();
        }

        [HttpGet("{id}", Name = "GetContactById")]
        public async Task<Document> GetContactById(string id)
        {
            return await _repository.GetDocumentById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Document>> CreateTestMessage2([FromBody] Document document)
        {
        

            await _repository.CreateDocument(document);

            return Ok();
        }

       [HttpPut("{id}", Name = "UpdateDocument")]
        public async Task<IActionResult> UpdateDocument(string id, [FromBody] Document document)
        {
            
            await _repository.UpdateDocument(id, document);

            return Ok();
        }

       [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocument(string id)
        {
            await _repository.DeleteDocument(id);
            return Ok();
        }

         [HttpGet("stage/{id}", Name = "GetAllDocumentsByDocumentId")]
         public async Task<ActionResult<IEnumerable<Document>>> GetAllDocumentsByDocumentId(string id)
         {
             
            var stageItem = await _repository.GetAllDocumentsByStageId(id);

            
             return Ok(_mapper.Map<IEnumerable<Document>>(stageItem));
         }
    }
}