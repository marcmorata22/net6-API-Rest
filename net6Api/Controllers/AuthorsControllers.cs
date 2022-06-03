using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using net6Api.Models;
using Microsoft.EntityFrameworkCore;



namespace net6Api.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsControllers : ControllerBase
    {

        private readonly ApplicationDbContext context;

        public AuthorsControllers(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<author>>> testGet()
        {
            return await context.Authors.Include(x => x.Books).ToListAsync();
        }

        [HttpGet("onlyOne")]//Podemos acceder a esta función desde dos rutas api/authors/onlyOne o -> comentario de abajo
        [HttpGet("first")]//api/authors/first
        public async Task<ActionResult<author>> GetFirst()
        {
            return await context.Authors.Include(x => x.Books).FirstOrDefaultAsync();
        }

        [HttpGet("{idAuthor:int}")]//We Set idAuthor here for required the var 
        public async Task<ActionResult<author>> GetbyId(int idAuthor)//ActionResult allows to return an author or some function like BadRequest or NotFound.
        {
            var exist = await context.Authors.AnyAsync
                (author => author.Id == idAuthor);

            if (!exist)
            {
                return NotFound();
            }

            return await context.Authors.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id == idAuthor);
        }

        [HttpPost]
        public async Task<ActionResult> testPost(author author)
        {
            context.Add(author);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{idAuthor:int}")]
        public async Task<ActionResult> testPut(author author, int idAuthor)
        {
            if (author.Id != idAuthor)
            {
                return BadRequest("id not the same with id URL");
            }

            var exist = await context.Authors.AnyAsync
                (author => author.Id == idAuthor);

            if (!exist)
            {
                return NotFound();
            }

            context.Update(author);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{idAuthor:int}")]// If we set a type for the var, then the function will wait that kind of var
        public async Task<ActionResult> testDelete(int idAuthor)
        {
            var exist = await context.Authors.AnyAsync
                (author => author.Id == idAuthor);

            if (!exist)
            {
                return NotFound();
            }

            //We set new author to instantiate an object of type author for send it to entityfraemwork and can delete it. 
            context.Remove(new author() { Id = idAuthor });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
