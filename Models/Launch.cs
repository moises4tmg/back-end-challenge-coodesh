using System.ComponentModel.DataAnnotations;
namespace DesafioCoodesh.Models
{
    public class Launch
    {
        [Key]
        public String? Id { get; set; }
        public String? Provider { get; set; }
    }
}