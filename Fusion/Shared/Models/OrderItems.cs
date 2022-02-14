using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class OrderItems
    {
        public int OrderID { get; set; }
        public string linenum { get; set; }
        public string PartID { get; set; }
        public string ItemDesc { get; set; }
        public Double Quantity { get; set; }
        public Double UnitPrice { get; set; }
        public string Units { get; set; }
        public string ExtValue { get; set; }
        public string CodeDesc { get; set; }
        public string GL_Code { get; set; }
        public string Taxable { get; set; }
        public Double QtyInvoiced { get; set; }
    }
}
