using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class Order
    {
        public string OrderID { get; set; }
        public string AreaName { get; set; }
        public string AreaID { get; set; }
        public string Description { get; set; }
        public int NAVJobID { get; set; }
        public string Customer_Job { get; set; }
        public int Status { get; set; }
        public string Notes { get; set; }
        public string TemplateName { get; set; }
        public string MeterAddress { get; set; }
        public string FieldTicket { get; set; }
        public decimal RetentionPercentage { get; set; }

        public string QuoteNotes { get; set; }
        public string CustID { get; set; }
        public string JobID { get; set; }
        public string JobName { get; set; }
        public string ScopeOfWork { get; set; }
        public string CustName { get; set; }
        public string CustAddress { get; set; }
        public string CustCity { get; set; }
        public string CustState { get; set; }
        public string CustZip { get; set; }
        public string JobAddress { get; set; }
        public string JobCity { get; set; }
        public string JobState { get; set; }
        public string JobZip { get; set; }
        public string Customer { get; set; }
        public string Job { get; set; }
        public int OrderTotal { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int QuotedBy { get; set; }
        public DateTime? QuotedOn { get; set; }
        public string AwardedBy { get; set; }
        public DateTime? AwardedOn { get; set; }
        public string ClosedBy { get; set; }
        public DateTime? ClosedOn { get; set; }
        public string TruckID { get; set; }
        public int Driver1 { get; set; }
        public int Driver2 { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public double StopNumber { get; set; }
        public string OpportunityID { get; set; }
        public string IsCreditHold { get; set; }
        public string DeliveryMethodType { get; set; }
        public string DeliveryMethodDesc { get; set; }
        public string SignRequirementType { get; set; }
        public string PORequirementType { get; set; }
        public string PORequirementAmount { get; set; }
        public string InvoiceAttachmentType { get; set; }
        public string isBRSApproved { get; set; }
        public string BRSApprovedBy { get; set; }
        public string BRSApprovedDate { get; set; }
        public string BillingRequirement1 { get; set; }
        public string BillingRequirement2 { get; set; }
        public string BillingRequirement3 { get; set; }
        public string BillingRequirement4 { get; set; }
        public string Contract { get; set; }
        public string TaxRate { get; set; }
    }
    public class SelectItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
