using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared
{
    public static class Constants
    {
        public const string Opportunity = "Opportunities";
        public const string Agreement = "Agreements";
        public const string WorkOrder = "Contracts";
        public const string Customer = "Customers";
        public const string WorkOrderInfos = "WorkOrders";
        public const string WorkOrderInfo = "WorkOrder";
        public const string Subcontractor = "Subcontractors";
        public const string Save = "Save";
        public const string Edit = "Edit";

        //Work Order 
        public const string ConfirmationMsg1 = "A work order has been submitted on ";
        public const string ConfirmationMsg2 = ". Please confirm the below information.";

        public const string SizeObserver = "Observe";
        public const string SizeObserverWidth = "ObservedWidth";

        public const string AddAssignment = "Add Assignment";
        public const string Assignments = "Assignments";

        //Opportunity
        public const string OppPromoteToAgmt = "Promote To Agreement";
        public const string OppAgmtPromoted = "Promoted To Agreement";
        public const string OppPromoteToWorkOrder = "Promote To Work Order";
        public const string OppWorkOrderPromoted = "Promoted To Work Order";
        public const string OppWOPromotionError = "Opportunity already promoted to Work Order";

        //Customer
        public const string CustomerStatusProspect = "PROSPECT";

        //Agreement Statuses
        public const string Draft = "DRAFT";
        public const string Awarded = "AWARDED";
        public const string Active = "ACTIVE";
        public const string Void = "VOID";
        public const string Lost = "LOST";
        public const string Closed = "CLOSED";

        //Printed Agreement 
        public const string TotalCost = "Total cost per year for the above scheduled maintenance is ";
        public const string AgmtTitle = "Generator Service Proposal";
        public const string AgmtUnitNullHours = "An issue occurred retrieving info for unit ";
        public const string AnnualService = " Annual Generator Service";
        public const string LoadBankService = "Loadbank Service";
        public const string RentalService = "Gen-Rental Service";
        public const string ATSService = "ATS Service and Testing";
        public const string SignatureHeading = "Please sign in acknowledge and agreement to Power Plus Terms and Conditions:";
        public const string TermsConditions = "Contract Terms and Conditions";

        public const string UnitMakeOther = "Other";
        public const int StdCableRunLength = 50;


        //Bing Maps
        public const string BingMapsKey = "Aqjz9SCdcKfF33dy6xQXXRYdyzebVaDS-FyMy982WOUhoIngtFm4yrfoCu35F-9N";

        public const string Service = "SERVICE";
        public const string Inspection = "INSPECTION";
        public const string LoadBank = "LOADBANK";
        public const string CSService = "LEVEL1";
        public const string CSInspection = "LEVEL2";

        //terms
        public const string AcceptanceofTerms = " By submitting a purchase order or accepting a price quotation of Power Plus LLC for the products or services described therein, Customer accepts and is bound to these standard terms and conditions.No additional or differing terms or conditions proposed or delivered by Customer, whether proposed or delivered verbally, through writing, electronic communication, facsimile, or any other means, shall retroactively alter Power Plus LLC's price quotation, contract agreement or these terms and conditions in any way. This Contract, including all of its terms and conditions, may only be amended in a writing signed by both parties";
        public const string MaintenanceAgreement = " Power Plus LLC agrees to maintain the equipment listed on the Asset Pricing Detail the \"System\"), according to the terms of this System Maintenance Agreement, including the Statement of Terms and Conditions set forth below (together, herein referred to as the \"Agreement\").";
        public const string TermofAgreement = " This Agreement shall commence (the \"Commencement Date\") on the later of: the \"Date of Agreement\" set forth above; or the date on which Power Plus LLC receives the initial payment for the service contract. For full service contracts, emergency coverage is not guaranteed until payment is received.";
        public const string PaymentTerms = " The service contract fee to be paid by the Customer in the amount set forth on the Asset Pricing Detail. Payment terms are net 30 days. Additional charges apply for contracts billed with extended payment terms. Late payments shall be subject to a late payment charge of 1.5% per month (18% per year) on any outstanding and delinquent balance. All quoted prices are in US dollars.";
        public const string Maintenance = " During the Initial Term and any Renewal Period, Power Plus LLC shall, in accordance with service levels purchased, furnish all necessary service, parts and materials to maintain the System in good working condition and repair. All parts replaced by Power Plus LLC shall comply with the equipment manufacturer’s published standards and/or specifications. See \"LIMITATION ON EQUIPMENT AND SERVICES COVERED BY THIS AGREEMENT\" below.Customer hereby grants Power Plus LLC full and reasonable access to the Service Location at which the System is located for the performance of these services.Power Plus LLC shall not be liable for damages to the equipment if Customer authorizes service, operation, and/or modification of said equipment by another party whereby it results in a shut down, removal or alteration of the equipment by the other party.In the event of such an occurrence, Power Plus LLC reserves the right to immediately terminate the Agreement, or if Power Plus LLC agrees to continue services, then Power Plus LLC will invoice separate of this Agreement for costs incurred to return the equipment to industry standards, in accordance with Power Plus LLC then current time and materials rates, and Power Plus LLC shall not be liable for future damages arising from the services performed by Customer-authorized third party.";
        public const string PreventativeMaintenanceVisits = " With respect to the Preventative Maintenance (PM) purchased under this Agreement, Power Plus LLC will use its best effort to schedule the PM visits as stated in the Contract Coverage section. Should the Customer cancel a confirmed PM visit with less than 5-business day’s notice prior to the scheduled service date, Customer shall be charged for any expenses incurred (including but not limited to, associated travel expenses and field engineer time). Should the Customer not permit a PM to be completed within ninety (90) days of the original proposed scheduled service date or prior to the Agreement End Date, Customer agrees that Power Plus LLC's obligation for that PM has been fulfilled.";
        public const string DiscontinuanceofParts = " For those systems deemed obsolete by the manufacturer (systems typically greater than 15 years of age), Power Plus LLC will continue to source replacement parts to the best of its ability. Should replacement parts for these obsolete systems not be available, Power Plus LLC will notify customer of such; and, where applicable, provide a pro-rata credit for the balance of the Agreement for the un-repaired obsolete system where parts coverage is included as an entitlement of the Agreement..";
        public const string TerminationofAgreement = " Power Plus LLC shall have the right to terminate this Agreement at any time and for any reason, upon thirty (30) days’ written notice to the Customer of Power Plus LLC 's intent to terminate, which notice shall specify the date of termination. If Power Plus LLC terminates this Agreement at any time prior to the end of the Initial Term or any Renewal Period, Power Plus LLC shall refund to the Customer a prorated amount of any prepaid Maintenance Charge, less any amounts which are owed to Power Plus LLC by Customer. Customer has the right to cancel this contract with a thirty (30) day written notice. Power Plus LLC will provide a refund in the form of an in-house credit, which will be less the costs associated with any performed Preventative Maintenance visits. Refund will be a prorated amount based on the number of months remaining in the current term of this agreement. If the customer has received their contracted Preventative Maintenance visits under this agreement on or before the cancellation date, the customer shall not be entitled to any refund of the annual";
        public const string CustomersRepresentationsWarrantiesandResponsibilities = " The customer hereby warrants that, prior to the effective date of this Agreement, the equipment which is the subject of this Agreement has been properly maintained and serviced in accordance with the manufacturer’s recommendations. If Power Plus LLC determines the equipment subject to this Agreement has not been properly maintained and/or has a pre-existing condition whereby Power Plus LLC must perform maintenance to bring the equipment up to such standards, then all costs shall be borne by the Customer at Power Plus LLC’s then current time and materials rates. Customer is liable for all parts, labor, and expenses (at Power Plus LLC’s then current time and materials rates) incurred by Power Plus LLC to evaluate, diagnose, and repair equipment found defective based on the terms of each equipment manufacturer’s warranty..";
        public const string LimitationonEquipmentandServicesCoveredbythisAgreement = " This Agreement, and Power Plus LLC 's obligations hereunder, covers only the equipment listed on the Asset Pricing Detail as well as subsequent Asset Pricing Details. Any equipment not listed on such Details may be serviced by Power Plus LLC at the Customer's request, or if deemed necessary by Power Plus LLC, but all such work shall be billed to Customer at Power Plus LLC s standard prevailing rates for such labor and materials, and Customer agrees to pay all such charges pursuant to the terms of this Agreement. This Agreement only covers labor and materials required due to damages to or failure of the System caused by wear and tear resulting from normal use, except battery and full capacitor replacements. This Agreement does not cover damages caused by misuse, negligence, accident, theft or unexplained loss, abuse, fire, flood, wind, lightning or other electrical surge, tornado, sandstorm, hail, explosion, earthquake, smoke, vandalism, terrorism, acts of God or public enemy, or improper wiring, installation, repair or alteration by anyone other than Power Plus LLC. Misuse shall apply whereby the equipment is operated in a condition extending outside of the equipment manufacturer’s recommended operating conditions or specifications, or exceeds the equipment’s original design limits. Examples include, but are not limited to, phase-imbalanced conditions (more than 20%). Repairs required by any of the above excepted causes will be made by Power Plus LLC at the standard prevailing rates for the necessary labor and materials shall be billed to the Customer, and Customer agrees to pay all such changes pursuant to the terms of this Agreement.";
        public const string LimitationDisclaimerofLiability = " Power Plus LLC shall not be liable for any indirect, incidental, special, or consequential damages, loss, or expense (including, but not limited to loss of use, revenue, data, or profit), directly or indirectly arising from use of, or inability to use, the system either separately or in combination with other equipment, or for personal injury or loss or destruction of other property, or from any other cause. Customer will pay any Municipal, County, State or Federal sales, excise or other taxes which may be levied upon the service or materials provided pursuant to this Agreement, and shall be responsible all costs associated with customer required union labor requirements. Customer shall indemnify Power Plus LLC against and hold Power Plus LLC harmless from any and all claims, actions, suits, proceeds, costs, expenses, damages and liabilities, including attorney's fees, claimed by any person, organization, association, or otherwise arising out of, or relating to the System, use, possession, operation and/or condition, thereof, arising out of any event on or after the date of this Agreement.";
        public const string Insurance = " Power Plus LLC maintains insurance coverage and limits as it deems necessary. Upon Owners request, Power Plus LLC (a) shall provide Owner with a Certificate of Liability Insurance, and (b) shall provide Owner with thirty (30) days advance notice of any cancellation or material change in coverage. If the Owner requires coverage or limits in addition to those in effect as of the date of the agreement, premiums for additional insurance shall be paid by the Owner.";
        public const string Assignment = " Customer may not transfer or assign its rights or obligations under this Agreement to a new owner of the System, without the written consent of Power Plus LLC. Power Plus LLC shall be under no obligations to continue this Agreement with a new owner of the System, but Power Plus LLC agrees to work with any new owner of the System to attempt to work out a separate System Maintenance Agreement which is acceptable to both the owner and Power Plus LLC.";
        public const string FailureofCustomertoMakeTimelyPayment = " If Customer fails to pay Power Plus LLC any amounts due pursuant to the terms of this Agreement within the time period required hereunder, Power Plus LLC may withhold services to be provided under this Agreement, even if this Agreement is still in effect. This shall be in addition to any other remedy which Power Plus LLC may have under this Agreement or under applicable law..";
        public const string GoverningLawandVenue = " This Agreement shall be governed by, and construed in accordance, with the law of the State of Delaware, and the venue of any court action initiated pursuant to this Agreement shall be held in the State of Delaware.";
        public const string LegalCosts = " In any legal proceedings instituted by Power Plus LLC for the enforcement of the terms and provisions of this Agreement where POWER PLUS LLC is the prevailing party, Power Plus LLC shall be reimbursed by Customer for all of its reasonable costs, expenses and attorneys’ fees..";
        public const string PartialInvalidity = " The terms and provisions of this Agreement shall be deemed separable. If any term or provision of this Agreement or the application thereof to any person or circumstances shall to any extent be invalid or unenforceable, the remainder of this Agreement, or the application of such term or provision to person(s) or circumstance(s) other than those as to which it is invalid or unenforceable, shall not be affected thereby. Each term and provision of this Agreement shall be enforceable to the fullest extent permitted by law.";
        public const string PurchaseOrders = " Notwithstanding terms and conditions contained in the Customer’s purchase order, the terms and conditions of this Agreement shall prevail..";
        public const string EntireAgreement = " THE TERMS OF THIS AGREEMENT INCLUDING THE REFERENCED DETAILS REPRESENT THE COMPLETE AND ENTIRE AGREEMENT BETWEEN POWER PLUS LLC AND CUSTOMER REGARDING THE MATTERS DESCRIBED HEREIN. NO VERBAL REPRESENTATION OF ANY SALESPERSON, AGENT, OFFICER, OR EMPLOYEE OF POWER PLUS LLC SHALL OPERATE TO VARY THE WRITTEN TERMS HEREOF. ANY ALTERATIONS OR MODIFICATIONS MUST BE IN WRITING, REFERENCE THIS AGREEMENT, AND BE SIGNED BY BOTH PARTIES.";
        public const string Waiver = " A waiver of the strict performance of any term of this Agreement by Power Plus LLC shall not be deemed waiver of any other provision of this Agreement.";
        public const string QuoteSumnmary = "**Please note all testing and servicing of units to be done incompliance to NFPA99,100and110**\n\nContractSignature-\nCustomer accepts this quotation and wishes to enter into a Service Contract for the services listed here in.Customer accepts the PowerPlus Standard Terms&Conditions(attached) of this Agreement.This price quotation is valid for 120 days.Thisform must be signed by anauthorized representative,and faxed to Power Plus headquarters at(800)784-8318 before the contract effective date.Signing this Agreement authorizes Power Plus to invoice for services defined here in and to utilize the provided PurchaseOrderNumber.Power Plus will invoice annually in advance.Additional charges apply for Agreements billed with extended payment terms.Please return a copy of your Tax Exempt Certificate,if applicable.If a PurchaseOrdernumber is not used,the customer authorizes and guarantees Power Plus the payment of such invoices by authority of the following signature.";

    }
}
