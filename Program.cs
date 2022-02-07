using Microsoft.EntityFrameworkCore;
using FluentScheduler;
using DesafioCoodesh.Models;
using DesafioCoodesh.Cron;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HerokuMySqlConnectionString");

builder.Services.AddControllers();
builder.Services.AddDbContext<ArticleContext>(opt =>
    opt.UseMySql(connectionString, new MySqlServerVersion(new Version()))
    // The following three options help with debugging
    //.LogTo(Console.WriteLine, LogLevel.Information)
    //.EnableSensitiveDataLogging()
    //.EnableDetailedErrors()
);

builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Console.WriteLine("[INFO] Executing Cron script for DB Sync");
ArticleContext? context = builder.Services.BuildServiceProvider().GetService<ArticleContext>();
//ArticleContext? context = app.Services.GetService<ArticleContext>();
if(context != null) 
    JobManager.Initialize(new JobRegistry(context));
else
    Console.WriteLine("[ERROR] Cron script inicialization error. Null DbContext");

app.Run();
