namespace WebApplication1.Models
{
    public class Customizations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }

    public class Customizations_Input
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int OrderId { get; set; }
    }
}
