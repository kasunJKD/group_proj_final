﻿namespace WebApplication1.Models
{
    public class Order_Customizations
    {
        public int Id { get; set; }
        public int CustomizationsId { get; set; }
        public Customizations Customizations { get; set; }
        public int OrderId { get; set; }

    }
}
