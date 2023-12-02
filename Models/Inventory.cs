using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableCount { get; set; } = 0;
        public double Price_Per_Unit { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set;}
    }
}
