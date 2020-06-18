using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleInvoice.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public double Discount { get; set; }
        //total after discount and tax
        public double Net { get; set; }

        //total after discount
        //Fixed
        //public double Tax { get; set; }

    }
}
