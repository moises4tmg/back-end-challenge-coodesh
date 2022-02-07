using FluentScheduler;
using DesafioCoodesh.Models;

namespace DesafioCoodesh.Cron;

public class JobRegistry : Registry
{
    public JobRegistry(ArticleContext context)
    {
        Schedule(() => new FeedDatabaseJob(context)).ToRunEvery(1).Days().At(9,0);
    }
}