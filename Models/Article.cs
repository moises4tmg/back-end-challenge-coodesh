using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DesafioCoodesh.Models
{
    public class Article
    {
        [KeyAttribute]
        public int Id { get; set; }
        public bool Featured { get; set; } = false;//default: false
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public string? NewsSite { get; set; }
        public string? Sumary { get; set; }
        public DateTime PublishedAt { get; set; } //Datetime format
        public List<Launch>? Launches { get; set; }
        public List<Event>? Events { get; set; }
    }
}