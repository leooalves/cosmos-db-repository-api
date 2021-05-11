
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CosmosRepository;
using System.Threading.Tasks;
using cosmos_db_repository_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace cosmos_db_repository_api.Controllers
{

    [ApiController]
    [Route("v1/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IRepository<Comment> _repository;

        public CommentController(IRepositoryFactory factory)
        {
            _repository = factory.RepositoryOf<Comment>();
        }

        [HttpGet]
        [Route("post/{postId}")]
        public async Task<ActionResult> GetCommentsFromPost(string postId)
        {
            IEnumerable<Comment> comments = await _repository.GetByQueryAsync($"SELECT * FROM c where c.postId = '{postId}' and c.type = 'Comment'");

            return Ok(comments);
        }

    
        [HttpGet]
        [Route("post/WithPredicate/{postId}")]
        public async Task<ActionResult> GetCommentsFromPostPredicate(string postId)
        {
            IEnumerable<Comment> comments = await _repository.GetAsync(x => x.postId == postId && x.Type == "Comment");

            return Ok(comments);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Post([FromBody] Comment model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);            

            try
            {
                await _repository.CreateAsync(model);
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não conseguiu criar o comentario" });
            }

            return Ok(model);
        }
    }

}
