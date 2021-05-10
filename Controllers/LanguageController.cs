
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

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Get()
        {
            IEnumerable<Language> languages = await _repository.GetByQueryAsync("select * from c");

            return Ok(languages);
        }



        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Post([FromBody] Language model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _repository.CreateAsync(model);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não conseguiu criar a linguagem" });
            }

            return Ok(model);
        }
    }

}
