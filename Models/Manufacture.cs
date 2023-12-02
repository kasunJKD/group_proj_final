namespace WebApplication1.Models
{
    public class Manufacture
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public StatusData Status { get; set; }
    }

    public enum StatusData
    {
        Pending,
        InProgress,
        Done
    }
}
