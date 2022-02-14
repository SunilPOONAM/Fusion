using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public partial class Opportunity
    {
        public int OppID { get; set; }
        public string CustomerID { get; set; }
        public string ContactName { get; set; }
        public string Nickname { get; set; }
        public string Description { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public Nullable<decimal> Value { get; set; }
        public string Status { get; set; }
        public string OppType { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Stage { get; set; }
        public Nullable<double> Probability { get; set; }
        public Nullable<System.DateTime> PromoteDate { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public Nullable<int> UnitQty { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddr1 { get; set; }
        public string CustomerAddr2 { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZip { get; set; }
        public string CustomerPhone { get; set; }
        public string Industry { get; set; }
        public Nullable<int> AgreementID { get; set; }
        public Nullable<int> ServiceCallID { get; set; }
        public string OpportunityOwner { get; set; }
        public string OpportunitySource { get; set; }
        
    }
}
