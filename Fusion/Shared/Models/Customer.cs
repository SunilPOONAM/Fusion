using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Area { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string CustomerStatus { get; set; }
        public string TimberlineShortName { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<int> EnteredBy { get; set; }
        public string DBIII_ { get; set; }
        public bool Taxable { get; set; }
        public bool AcceptDamageWaiver { get; set; }
        public string CustomerNotes { get; set; }
        public bool PowerManagement { get; set; }
        public Nullable<System.DateTime> PMCStartDate { get; set; }
        public Nullable<System.DateTime> PMCEndDate { get; set; }
        public Nullable<float> PMCMonthlyRate { get; set; }
        public string Department { get; set; }
        public bool HasCustomPricing { get; set; }
        public byte[] upsize_ts { get; set; }
        public int CustomRentalPriceID { get; set; }
        public int CustomFSPriceID { get; set; }
        public Nullable<int> BillingContactID { get; set; }
        public Nullable<bool> IsTelecomCustomer { get; set; }
        public Nullable<int> AccountRep { get; set; }
        public Nullable<float> FuelSurcharge { get; set; }
        public string ParentCustID { get; set; }
        public Nullable<bool> TaxRateLocked { get; set; }
        public Nullable<decimal> ContractedLaborRate { get; set; }
        public Nullable<bool> LaborRateIsFixed { get; set; }
        public string EmailTechComplete { get; set; }
        public string Category { get; set; }
        public Nullable<bool> CreditHold { get; set; }
        public Nullable<int> MasterCustomerID { get; set; }
        public Nullable<decimal> MileageRate { get; set; }
        public Nullable<bool> ServiceCall { get; set; }
        public Nullable<bool> Repair { get; set; }
        public Nullable<bool> Refuel { get; set; }
        public Nullable<bool> Maintenance { get; set; }
        public string CustomerType { get; set; }
        public string BillingRequirement1 { get; set; }
        public string BillingRequirement2 { get; set; }
        public string BillingRequirement3 { get; set; }
        public string BillingRequirement4 { get; set; }
        public string BillToID { get; set; }
        public Nullable<bool> OverrideDefaultTC { get; set; }
        public Nullable<bool> Insurance { get; set; }
        public string InsuranceByDate { get; set; }
        public Nullable<System.DateTime> InsuranceExpiration { get; set; }
        public string InsuranceContact { get; set; }
        public Nullable<bool> SurchargeServiceCalls { get; set; }
        public Nullable<bool> SurchargeRepairs { get; set; }
        public Nullable<bool> SurchargeMaint { get; set; }
        public Nullable<bool> SurchargeFuel { get; set; }
        public Nullable<bool> SurchargeRental { get; set; }
        public Nullable<bool> PrevailingWage { get; set; }
        public string Email { get; set; }
        public Nullable<Int16> DefaultTerms { get; set; }
        public string Nickname { get; set; }
        public Nullable<int> DeliveryMethodType { get; set; }
        public string DeliveryMethodDesc { get; set; }
        public Nullable<int> SignRequirementType { get; set; }
        public Nullable<int> PORequirementType { get; set; }
        public Nullable<decimal> PORequirementAmount { get; set; }
        public Nullable<int> InvoiceAttachmentType { get; set; }
        public string TaxExemptStates { get; set; }
        public Nullable<bool> IsArchived { get; set; }
        public Nullable<bool> IsHeadQuarter { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> IsCreditHold { get; set; }
        public bool IsTaxExempt { get; set; }
    }
}
