using Xunit;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioCoodesh.Controllers;
using DesafioCoodesh.Models;

public class EndpointTest
{
    private async Task<ArticleContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ArticleContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new ArticleContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Articles.CountAsync() <= 0)
        {
            for (int i = 1; i <= 10; i++)
            {
                databaseContext.Articles.Add(new Article()
                {
                    Id = i,
                    Featured = false,
                    Title = "title n"+i,
                    Url = "https://fortestingpurposes.com/testpage"+i,
                    ImageUrl = "https://imgur/test"+i,
                    NewsSite = "fortestingpurposes.com",
                    Sumary = "",
                    PublishedAt = DateTime.UtcNow
                });
                await databaseContext.SaveChangesAsync();
            }
        }
        return databaseContext;
    }

    // Endpint: [GET] /
    [Fact] 
    public async Task Index_ShouldReturnOk()
    {
        // Arrange
        var dbcontext = await GetDatabaseContext();
        var controller = new ArticlesController(dbcontext);

        // Act
        var result = controller.Index();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    // Endpint: [GET] /articles/ 
    [Fact] 
    public async Task getArticles_ShouldReturnOk()
    {
        // Arrange
        var dbcontext = await GetDatabaseContext();
        var controller = new ArticlesController(dbcontext);

        // Act
        var result = await controller.getArticles();
        
        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    // Endpint: [GET] /articles/ 
    // trying to take 101 items on page 1, the max allowed is 100 items per request
    [Fact] 
    public async Task getArticles_ShouldReturnBadRequest()
    {
        // Arrange
        ArticleContext dbcontext = await GetDatabaseContext();
        ArticlesController controller = new ArticlesController(dbcontext);
        int page = 1;
        int items = 101;

        // Act
        var result = await controller.getArticles(page, items);
        
        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    // Endpint: [GET] /articles/{id} 
    [Fact] 
    public async Task getArticle_ShouldReturnOk()
    {
        // Arrange
        ArticleContext dbcontext = await GetDatabaseContext();
        ArticlesController controller = new ArticlesController(dbcontext);
        int id = 1;

        // Act
        var result = await controller.getArticle(id);
        
        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    // Endpint: [GET] /articles/{id} 
    // tring to get an article that does not exist
    [Fact] 
    public async Task getArticle_ShouldReturnNotFound()
    {
        // Arrange
        ArticleContext dbcontext = await GetDatabaseContext();
        ArticlesController controller = new ArticlesController(dbcontext);
        int id = 0;
        
        // Act
        var result = await controller.getArticle(id);
        
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }    

    // Endpint: [POST] /articles/
    [Fact] 
    public async Task postArticle_ShouldReturnCreatedAtAction()
    {
        // Arrange
        ArticleContext dbcontext = await GetDatabaseContext();
        ArticlesController controller = new ArticlesController(dbcontext);
        Article articleToBePosted = new Article()
        {
            Featured = false,
            Title = "POST TEST",
            Url = "https://fortestingpurposes.com/posttestpage",
            ImageUrl = "https://imgur/posttest",
            NewsSite = "fortestingpurposes.com",
            Sumary = "",
            PublishedAt = DateTime.UtcNow
        };
        
        // Act
        var result = await controller.postArticle(articleToBePosted);
        
        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }  

    // Endpint: [PUT] /articles/{id}
    // tries to update an article
    [Fact] 
    public async Task putArticle_ShouldReturnNoContentResult()
    {
        // Arrange
        ArticleContext dbcontext = await GetDatabaseContext();
        ArticlesController controller = new ArticlesController(dbcontext);
        Article articleToBePosted = new Article()
        {
            Id = 2,
            Featured = false,
            Title = "PUT TEST",
            Url = "https://fortestingpurposes.com/puttestpage",
            ImageUrl = "https://imgur/puttest",
            NewsSite = "fortestingpurposes.com",
            Sumary = "",
            PublishedAt = DateTime.UtcNow
        };
        int id = 2;
        
        // Act
        var result = await controller.putArticle(id, articleToBePosted);
        
        // Assert
        Assert.IsType<ObjectResult>(result);
    }  

    // Endpint: [PUT] /articles/{id}
    // tries to update an invadid article
    [Fact] 
    public async Task putArticle_ShouldReturnBadRequestObjectResult()
    {
        // Arrange
        ArticleContext dbcontext = await GetDatabaseContext();
        ArticlesController controller = new ArticlesController(dbcontext);
        Article articleToBePosted = new Article()
        {
            Id = 1,
            Featured = false,
            Title = "PUT TEST",
            Url = "https://fortestingpurposes.com/puttestpage",
            ImageUrl = "https://imgur/puttest",
            NewsSite = "fortestingpurposes.com",
            Sumary = "",
            PublishedAt = DateTime.UtcNow
        };
        int id = 0;
        
        // Act
        var result = await controller.putArticle(id, articleToBePosted);
        
        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }  

    // Endpint: [PUT] /articles/{id}
    // tries to update an invadid article
    [Fact] 
    public async Task putArticle_ShouldReturnNotFoundObjectResult()
    {
        // Arrange
        ArticleContext dbcontext = await GetDatabaseContext();
        ArticlesController controller = new ArticlesController(dbcontext);
        Article articleToBePosted = new Article()
        {
            Id = 0,
            Featured = false,
            Title = "PUT TEST",
            Url = "https://fortestingpurposes.com/puttestpage",
            ImageUrl = "https://imgur/puttest",
            NewsSite = "fortestingpurposes.com",
            Sumary = "",
            PublishedAt = DateTime.UtcNow
        };
        int id = 0;
        
        // Act
        var result = await controller.putArticle(id, articleToBePosted);
        
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }  

    // Endpint: [DELETE] /articles/{id}
    // tries to delete an article
    [Fact] 
    public async Task deleteArticle_ShouldReturnNoContentResult()
    {
        // Arrange
        ArticleContext dbcontext = await GetDatabaseContext();
        ArticlesController controller = new ArticlesController(dbcontext);
        int id = 1;
        
        // Act
        var result = await controller.deleteArticle(id);
        
        // Assert
        Assert.IsType<NoContentResult>(result);
    }  

    // Endpint: [DELETE] /articles/{id}
    // tries to delete an article that does not exist
    [Fact] 
    public async Task deleteArticle_ShouldReturnNotFoundResult()
    {
        // Arrange
        ArticleContext dbcontext = await GetDatabaseContext();
        ArticlesController controller = new ArticlesController(dbcontext);
        int id = 0;
        
        // Act
        var result = await controller.deleteArticle(id);
        
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }  

}