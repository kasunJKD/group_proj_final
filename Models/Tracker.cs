namespace WebApplication1.Models
{
    public class Tracker
    {
        public string userId { get; set; }
        public Order order { get; set; }
        public Manufacture manufacture { get; set; }
        public Shipping shipping { get; set; }

    }
}
