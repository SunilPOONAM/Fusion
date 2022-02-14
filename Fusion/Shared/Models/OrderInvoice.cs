using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class OrderInvoice
    {
        public string InvoiceName { get; set; }
        public string CodeDesc { get; set; }
        public string InvoiceDate { get; set; }
        public Double InvoiceTotal { get; set; }
        public string InvDesc { get; set; }
        public string InvNotes { get; set; }
        public int InvoiceID { get; set; }
        public int OrderID { get; set; }
    }
}
