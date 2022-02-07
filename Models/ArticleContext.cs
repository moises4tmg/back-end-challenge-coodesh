using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DesafioCoodesh.Models
{
    public class ArticleContext : DbContext
    {
        public ArticleContext(DbContextOptions<ArticleContext> options) : base(options){}

        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<Launch> Launches { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
    }
}