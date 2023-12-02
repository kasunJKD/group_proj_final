namespace WebApplication1.Models
{
    public class Shipping
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime Estimated_DateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string Address { get; set; }
        public StatusData Status { get; set; }
    }
}
