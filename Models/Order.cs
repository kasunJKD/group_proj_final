﻿namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int OrderedModelId { get; set; }
        public PlaneModels OrderedModel { get; set; }
        public double Customization_Price { get; set; }
        public double Total_Price { get;set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public StatusData Status { get; set; }

    }
}
