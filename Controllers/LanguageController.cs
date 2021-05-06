
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;
using System.Threading.Tasks;
using cosmos_db_repository_api.Models;
using System;
using System.Collections.Generic;

namespace cosmos_db_repository_api.Controllers
{

    [ApiController]
    [Route("v1/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly IRepository<Language> _repository;

        public LanguageController(IRepositoryFactory factory)
        {
            _repository = factory.RepositoryOf<Language>();
        }
        
        [HttpGet(Name = nameof(GetLanguages))]
        public ValueTask<IEnumerable<Language>> GetLanguages() => 
            _repository.GetByQueryAsync("select * from c");

        
        [HttpPost(Name = nameof(PostLanguages))]
        public ValueTask<IEnumerable<Language>> PostLanguages([FromBody] params Language[] languages) =>
            _repository.CreateAsync(languages);


    }

}
