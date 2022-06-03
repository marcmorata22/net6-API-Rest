using Microsoft.AspNetCore.Mvc;
using net6Api.Models;
using Microsoft.EntityFrameworkCore;

namespace net6Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BooksController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        [HttpGet("{idBook:int}")]//We Set idAuthor here for required the var 
        public async Task<ActionResult<Book>> GetBookbyId(int idBook)
        {
            
            return await context.Books.Include(x => x.author).FirstOrDefaultAsync(x => x.id == idBook);
        }

        [HttpPost]
        public async Task<ActionResult> PostBook(Book book)
        {
            var exist = await context.Authors.AnyAsync
                (x => x.Id == book.AuthorId);

            if(!exist)
            {
                //$ let set var on the String
                return BadRequest($"no exist author: {book.AuthorId}");
            }

            context.Add(book);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
