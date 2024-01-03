using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Inventory
    {
         [DisplayName("ID")]
         public int Id { get; set; }
         [DisplayName("PRODUCT NAME")]
         public string Name { get; set; }
         [DisplayName("AVAILABLE QUANTITY")]
         public int AvailableCount { get; set; } = 0;
         [DisplayName("PRICE PER UNIT")]
         public double Price_Per_Unit { get; set; }
         [DisplayName("CREATED TIME")]
         public DateTime CreatedDateTime { get; set; }
         [DisplayName("UPDATED TIME")]
         public DateTime UpdatedDateTime { get; set;}
    }
}
