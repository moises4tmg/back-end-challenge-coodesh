using FluentScheduler;
using Newtonsoft.Json;
using DesafioCoodesh.Models;

namespace DesafioCoodesh.Cron;
    
public class FeedDatabaseJob : IJob
{
    private readonly ArticleContext _context;

    public FeedDatabaseJob(ArticleContext context)
    {
        _context = context;
    }

    public static async Task FeedDatabase(ArticleContext context)
    {
        String url = "https://api.spaceflightnewsapi.net/v3/articles/";
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(url);
        int articleId = 1;
        do{
            if((context.Articles.FirstOrDefault(x => x.Id == articleId) == null))
            {
                response = await httpClient.GetAsync(url+articleId);
                if(!response.IsSuccessStatusCode)
                    break;
                String apiArticle = await response.Content.ReadAsStringAsync();
                Console.WriteLine("[CRON LOG] WORKING ON ITEM ID"+articleId);
                Article? deserializedJson = JsonConvert.DeserializeObject<Article>(apiArticle);
                if (deserializedJson != null)
                {
                    if(deserializedJson.Launches != null && deserializedJson.Launches.Count() > 0)
                    {
                        if(context.Launches.FirstOrDefault(x => Equals(x.Id, deserializedJson.Launches[0].Id)) != null)
                        {
                            deserializedJson.Launches[0].Id = "";
                            deserializedJson.Launches[0].Provider = "";
                        }
                    }
                    if(deserializedJson.Events != null && deserializedJson.Events.Count() > 0)
                    {
                        if(context.Events.FirstOrDefault(x => Equals(x.Id, deserializedJson.Events[0].Id)) != null)
                        {
                            deserializedJson.Events[0].Id = "";
                            deserializedJson.Events[0].Provider = "";
                        }    
                    }
                    context.Add<Article>(deserializedJson);
                }
                await context.SaveChangesAsync();
                articleId++;
                Console.WriteLine("[CRON LOG] SAVING ITEM ID"+articleId);
            }else{
                Console.WriteLine("[CRON LOG] ITEM ID"+articleId+" ALREADY EXISTS");
                articleId++;
                continue;
            }
        }
        while(response.IsSuccessStatusCode);
    }

    public void Execute(){
        try{
            FeedDatabase(_context).Wait(); // FluentScheduler does not support async jobs. Not yet ;)
            Console.WriteLine("[CRON LOG] Successfully updated the database. Next Data Sync is scheduled for tomorrow 09:00AM");
        }catch(Exception error){
            Console.WriteLine("[CRON ERROR] Error while updating database. "+error);
        }
        
    }
}