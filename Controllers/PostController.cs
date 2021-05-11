
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;
using System.Threading.Tasks;
using cosmos_db_repository_api.Models;
using System.Collections.Generic;
using System;

namespace cosmos_db_repository_api.Controllers
{

    [ApiController]
    [Route("v1/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IRepository<Post> _repository;

        public PostController(IRepositoryFactory factory)
        {
            _repository = factory.RepositoryOf<Post>();
        }

        [HttpGet]
        [Route("{postId}")]
        public async Task<ActionResult> GetById(string postId)
        {
            IEnumerable<Post> posts = await _repository.GetByQueryAsync($"SELECT * FROM c where c.postId = '{postId}' and c.type = 'Post'");

            return Ok(posts);
        }


        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Get()
        {
            IEnumerable<Post> posts = await _repository.GetByQueryAsync("select * from c where c.type = 'Post'");

            return Ok(posts);
        }


        [HttpGet]
        [Route("Linq")]
        public async Task<ActionResult> GetWithLinq()
        {
            IEnumerable<Post> posts = await _repository.GetAsync(x => x.DataCriacao > new DateTime(2020,01,01) );

            return Ok(posts);
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Post([FromBody] Post model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _repository.CreateAsync(model);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não conseguiu criar o post" });
            }

            return Ok(model);
        }
    }

}
