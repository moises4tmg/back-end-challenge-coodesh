using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioCoodesh.Models;

namespace DesafioCoodesh.Controllers;

[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly ArticleContext _context;

    public ArticlesController(ArticleContext context)
    {
        _context = context;
    }

    // [GET] /
    [HttpGet]
    [Route("")]
    public OkObjectResult Index()
    {
        return new OkObjectResult(new { 
            Message="Back-end Challenge 2021 🏅 - Space Flight News"
        });
    }

    // [GET] /articles/
    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> getArticles([FromQuery]int page=0, [FromQuery]int take=10)
    {
        if(take > 100)
            return BadRequest(new {message="Too many requests"});
        var articles = await _context
            .Articles
            .AsNoTracking()
            .Skip(page)
            .Take(take)
            .ToListAsync();
        return Ok(articles);
    }

    // [GET] /articles/{id}
    [HttpGet]
    [Route("[controller]/{id}")]
    public async Task<IActionResult> getArticle([FromRoute]int id)
    {
        var article = await _context
            .Articles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        if(article == null)
            return NotFound();
        return Ok(article);
    }

    // [POST] /articles/
    [HttpPost]
    [Route("[controller]")]
    public async Task<IActionResult> postArticle(Article article)
    {
        if(!ModelState.IsValid)
            return BadRequest("Invadid data");
        
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(getArticle),
            new { id = article.Id },
            article
        );
    }

    // [PUT] /articles/{id}
    [HttpPut]
    [Route("[controller]/{id}")]
    public async Task<IActionResult> putArticle([FromRoute]int id, [FromBody]Article article)
    {
        if(id != article.Id)
            return BadRequest("Invalid data");
        if(await _context.Articles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) == null)
            return NotFound();

        _context.ChangeTracker.Clear();
        _context.Entry(article).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // [DELETE] /articles/{id}
    [HttpDelete]
    [Route("[controller]/{id}")]
    public async Task<IActionResult> deleteArticle(int id){
        var article = await _context.Articles.FindAsync(id);

        if(article == null)
            return NotFound();
        
        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}