namespace WebApplication1.Models
{
    public class PlaneModels
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Inventory FuselageInventory { get; set; }
        public Inventory WingsInventory { get; set; }
        public int Wing_Count { get; set; }
        public Inventory EngineInventory { get; set; }
        public int Engine_Count { get; set; }
        public double Max_Range { get; set; }
        public double Length { get; set;}
        public double Width { get; set;}
        public double BasePrice { get; set;}
        public string? Image_url { get; set;}

        public ICollection<Inventory> Inventories { get; set; }


    }
}
