using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class InventoryReport
    {
        public string ProductName { get; set; }
        public int InitialQuantity { get; set; }
        public int StockInQuantity { get; set; }
        public int StockOutQuantity { get; set; }
        //public int CurrentQuantity { get; set; }

        public int CurrentStock => (InitialQuantity + StockInQuantity) - StockOutQuantity;

    }
}