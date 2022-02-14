using Fusion.Server.Helper;
using Fusion.Shared;
using Fusion.Shared.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Fusion.Server.Service.imp
{
    public class ManageCustomer : CustomerBase
    {
        #region Declaration
        private readonly SqlDataAccess _db = new SqlDataAccess();
        DataTable dtContainer;
        #endregion

        public override Customer GetCustomerById(string id)
        {
            Customer retval = new Customer();
            dtContainer = new DataTable();
            try
            {
                string query = "Select * From Customers Where CustomerID='" + id + "'";
                dtContainer = _db.DataTable_return(query);
                if (dtContainer.Rows.Count > 0)
                {
                    retval = GenerateSQL.GetItem<Customer>(dtContainer.Rows[0]);
                }
                else
                {
                    retval = null;
                }
            }
            catch (Exception ex)
            {
            }
            return retval;
        }

        public override List<Customer> GetCustomerList()
        {
            List<Customer> retval = new List<Customer>();
            retval = null;
            dtContainer = new DataTable();
            try
            {
                string query = "Select * From Customers Order By CustomerName";
                dtContainer = _db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Customer>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }

        public override List<Customer> GetSearchedCustomers(string searchstr)
        {
            List<Customer> retval = new List<Customer>();
            dtContainer = new DataTable();
            try
            {
                string query = "select * from Customers where CustomerID LIKE '%" + searchstr + "%' OR CustomerName LIKE '%" + searchstr + "%' OR Address1 LIKE '%" + searchstr + "%' OR City LIKE '%" + searchstr + "%' ORDER BY CustomerName";
                dtContainer = _db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Customer>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }

        public override bool AddCustomer(Customer cust)
        {
            bool retval = false;
            int result = 0;
            try
            {
                cust.SetPropertyValues();
                string EntryDate = cust.EntryDate != null ? Convert.ToDateTime(cust.EntryDate).ToString() : "";
                string query = "insert into Customers(CustomerID,CustomerName,Area,Address1,Address2,City,State,Zip,Phone,Fax,CustomerStatus,TimberlineShortName,EntryDate,EnteredBy,DBIII#," +
                    " Taxable,AcceptDamageWaiver,CustomerNotes,PowerManagement,PMCStartDate,PMCEndDate,PMCMonthlyRate,Department,HasCustomPricing,CustomRentalPriceID," +
                    " CustomFSPriceID,BillingContactID,IsTelecomCustomer,AccountRep,FuelSurcharge,TaxRateLocked,ContractedLaborRate,LaborRateIsFixed,EmailTechComplete," +
                    " Category,CreditHold,MasterCustomerID,MileageRate,ServiceCall,Repair,Refuel,Maintenance,CustomerType,BillingRequirement1,BillingRequirement2,BillingRequirement3," +
                    " BillingRequirement4,BillToID,OverrideDefaultTC,Insurance,InsuranceByDate,InsuranceExpiration,InsuranceContact,SurchargeServiceCalls,SurchargeRepairs,SurchargeMaint," +
                    " SurchargeFuel,SurchargeRental,PrevailingWage,Email,DefaultTerms,Nickname,DeliveryMethodType,DeliveryMethodDesc,SignRequirementType,PORequirementType,PORequirementAmount," +
                    " InvoiceAttachmentType,TaxExemptStates,IsArchived,IsHeadQuarter,IsApproved,IsCreditHold,IsTaxExempt)" +
                    " values ('" + cust.CustomerID + "','" + cust.CustomerName + "','" + cust.Area + "','" + cust.Address1 + "'," +
                    " '" + cust.Address2 + "','" + cust.City + "','" + cust.State + "','" + cust.Zip + "','" + cust.Phone + "','" + cust.Fax + "'," +
                    " '" + cust.CustomerStatus + "','" + cust.TimberlineShortName + "','" + EntryDate + "'," + cust.EnteredBy + ",'" + cust.DBIII_ + "','" + cust.Taxable + "'," +
                    " '" + cust.AcceptDamageWaiver + "','" + cust.CustomerNotes + "','" + cust.PowerManagement + "','" + cust.PMCStartDate + "','" + cust.PMCEndDate + "'," + cust.PMCMonthlyRate + "," +
                    " '" + cust.Department + "','" + cust.HasCustomPricing + "'," + cust.CustomRentalPriceID + "," + cust.CustomFSPriceID + "," + cust.BillingContactID + ",'" + cust.IsTelecomCustomer + "'," +
                    " " + cust.AccountRep + "," + cust.FuelSurcharge + ",'" + cust.TaxRateLocked + "'," + cust.ContractedLaborRate + ",'" + cust.LaborRateIsFixed + "','" + cust.EmailTechComplete + "'," +
                    " '" + cust.Category + "','" + cust.CreditHold + "'," + cust.MasterCustomerID + "," + cust.MileageRate + ",'" + cust.ServiceCall + "','" + cust.Repair + "'," +
                    " '" + cust.Refuel + "','" + cust.Maintenance + "','" + cust.CustomerType + "','" + cust.BillingRequirement1 + "','" + cust.BillingRequirement2 + "','" + cust.BillingRequirement3 + "'," +
                    " '" + cust.BillingRequirement4 + "','" + cust.BillToID + "','" + cust.OverrideDefaultTC + "','" + cust.Insurance + "','" + cust.InsuranceByDate + "','" + cust.InsuranceExpiration + "'," +
                    " '" + cust.InsuranceContact + "','" + cust.SurchargeServiceCalls + "','" + cust.SurchargeRepairs + "','" + cust.SurchargeMaint + "','" + cust.SurchargeFuel + "','" + cust.SurchargeRental + "'," +
                    " '" + cust.PrevailingWage + "','" + cust.Email + "','" + cust.DefaultTerms + "','" + cust.Nickname + "','" + cust.DeliveryMethodType + "','" + cust.DeliveryMethodDesc + "'," +
                    " '" + cust.SignRequirementType + "','" + cust.PORequirementType + "','" + cust.PORequirementAmount + "','" + cust.InvoiceAttachmentType + "','" + cust.TaxExemptStates + "','" + cust.IsArchived + "'," +
                    " '" + cust.IsHeadQuarter + "','" + cust.IsApproved + "','" + cust.IsCreditHold + "','" + cust.IsTaxExempt + "')";
                result = _db.ExecuteNonQuery_IUD(query);
                if (result > 0)
                {
                    retval = true;
                }
            }
            catch (Exception ex)
            {

            }
            return retval;
        }

        public override bool UpdateCustomer(Customer cust)
        {
            bool retval = false;
            int result = 0;
            try
            {
                cust.SetPropertyValues();
                string EntryDate = cust.EntryDate != null ? Convert.ToDateTime(cust.EntryDate).ToString() : "";
                string Query = "UPDATE Customers SET CustomerName ='" + cust.CustomerName + "',Area ='" + cust.Area + "',Address1 ='" + cust.Address1 + "'," +
                    " Address2 ='" + cust.Address2 + "',City ='" + cust.City + "',State ='" + cust.State + "',Zip = '" + cust.Zip + "', Phone ='" + cust.Phone + "', Fax = '" + cust.Fax + "', " +
                    " CustomerStatus = '" + cust.CustomerStatus + "', TimberlineShortName = '" + cust.TimberlineShortName + "',EntryDate = '" + EntryDate + "'," +
                    " EnteredBy =" + cust.EnteredBy + ",DBIII# ='" + cust.DBIII_ + "',Taxable ='" + cust.Taxable + "',AcceptDamageWaiver ='" + cust.AcceptDamageWaiver + "'," +
                    " CustomerNotes ='" + cust.CustomerNotes + "',PowerManagement ='" + cust.PowerManagement + "',PMCStartDate ='" + cust.PMCStartDate + "'," +
                    " PMCEndDate ='" + cust.PMCEndDate + "',PMCMonthlyRate =" + cust.PMCMonthlyRate + ",Department ='" + cust.Department + "'," +
                    " HasCustomPricing ='" + cust.HasCustomPricing + "',CustomRentalPriceID =" + cust.CustomRentalPriceID + ",CustomFSPriceID =" + cust.CustomFSPriceID + "," +
                    " BillingContactID =" + cust.BillingContactID + ",IsTelecomCustomer ='" + cust.IsTelecomCustomer + "',AccountRep =" + cust.AccountRep + "," +
                    " FuelSurcharge =" + cust.FuelSurcharge + ",TaxRateLocked ='" + cust.TaxRateLocked + "'," +
                    " ContractedLaborRate =" + cust.ContractedLaborRate + ",LaborRateIsFixed ='" + cust.LaborRateIsFixed + "',EmailTechComplete ='" + cust.EmailTechComplete + "'," +
                    " Category ='" + cust.Category + "',CreditHold ='" + cust.CreditHold + "',MasterCustomerID =" + cust.MasterCustomerID + ",MileageRate =" + cust.MileageRate + "," +
                    " ServiceCall ='" + cust.ServiceCall + "',Repair ='" + cust.Repair + "',Refuel ='" + cust.Refuel + "',Maintenance ='" + cust.Maintenance + "'," +
                    " CustomerType ='" + cust.CustomerType + "',BillingRequirement1 ='" + cust.BillingRequirement1 + "',BillingRequirement2 ='" + cust.BillingRequirement2 + "'," +
                    " BillingRequirement3 ='" + cust.BillingRequirement3 + "',BillingRequirement4 ='" + cust.BillingRequirement4 + "',BillToID ='" + cust.BillToID + "'," +
                    " OverrideDefaultTC ='" + cust.OverrideDefaultTC + "',Insurance ='" + cust.Insurance + "',InsuranceByDate ='" + cust.InsuranceByDate + "'," +
                    " InsuranceExpiration ='" + cust.InsuranceExpiration + "',InsuranceContact ='" + cust.InsuranceContact + "',SurchargeServiceCalls ='" + cust.SurchargeServiceCalls + "'," +
                    " SurchargeRepairs ='" + cust.SurchargeRepairs + "',SurchargeMaint ='" + cust.SurchargeMaint + "',SurchargeFuel ='" + cust.SurchargeFuel + "'," +
                    " SurchargeRental ='" + cust.SurchargeRental + "',PrevailingWage ='" + cust.PrevailingWage + "',Email ='" + cust.Email + "',DefaultTerms ='" + cust.DefaultTerms + "', " +
                    " Nickname ='" + cust.Nickname + "',DeliveryMethodType ='" + cust.DeliveryMethodType + "',DeliveryMethodDesc ='" + cust.DeliveryMethodDesc + "',SignRequirementType ='" + cust.SignRequirementType + "', " +
                    " PORequirementType ='" + cust.PORequirementType + "',PORequirementAmount ='" + cust.PORequirementAmount + "',InvoiceAttachmentType ='" + cust.InvoiceAttachmentType + "', " +
                    " TaxExemptStates ='" + cust.TaxExemptStates + "',IsArchived ='" + cust.IsArchived + "',IsHeadQuarter ='" + cust.IsHeadQuarter + "',IsApproved ='" + cust.IsApproved + "', " +
                    " IsCreditHold ='" + cust.IsCreditHold + "',IsTaxExempt ='" + cust.IsTaxExempt + "' where CustomerID='" + cust.CustomerID + "'";

                result = _db.ExecuteNonQuery_IUD(Query);
                if (result > 0)
                {
                    retval = true;
                }
            }
            catch (Exception ex)
            {

            }
            return retval;
        }

        public override List<vwAssignment> GetOpenOpportunityByCustomerId(string customerId)
        {
            List<vwAssignment> retval = new List<vwAssignment>();
            dtContainer = new DataTable();
            try
            {
                string JoinQuery = "select a.FollowUp as FollowUp,a.Role as Role,o.Value as Value,o.Nickname as Nickname,o.OppType as OppType,o.CustomerName as CustomerName from Assignments a join Opportunities o on a.ObjectID = o.OppID where o.CustomerID ='" + customerId + "' and a.AssignedObjectType ='Opportunities'";
                dtContainer = _db.DataTable_return(JoinQuery);
                retval = GenerateSQL.ConvertToList<vwAssignment>(dtContainer);
            }
            catch (Exception ex)
            {

            }
            return retval;
        }

        public override List<Opportunity> GetCustomerOpportunitiesList(string CustomerId)
        {
            List<Opportunity> retval = new List<Opportunity>();
            retval = null;
            dtContainer = new DataTable();
            try
            {
                string query = "Select * from Opportunities where CustomerID='" + CustomerId + "'";
                dtContainer = _db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Opportunity>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }

        public override List<Agreement> GetAgreementsByCustomerId(string CustomerId)
        {
            List<Agreement> retval = new List<Agreement>();
            dtContainer = new DataTable();

            try
            {
                string query = "select a.* from Agreements a join Customers cust on a.CustomerID=cust.CustomerID where cust.CustomerID ='" + CustomerId + "'";
                dtContainer = _db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Agreement>(dtContainer);
            }
            catch (Exception ex)
            {
            }

            return retval;
        }
        public override List<Contact> GetCustomerContactInfoByCustomerID(string customerId)
        {
            List<Contact> retval = new List<Contact>();
            dtContainer = new DataTable();
            try
            {
                string query = " select * from contacts where CustomerID='" + customerId + "'";
                dtContainer = _db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Contact>(dtContainer);
            }
            catch (Exception ex)
            {

            }
            return retval;
        }
        public override int SaveContactInfo(Contact contact)
        {
            int retval = -1;
            try
            {
                string InsertQuery = "insert into Contacts(CustomerID,ContactName,ContactTitle,ContactType,Phone,Cell,Fax,Email,ContactComment,Inactive) values ('" + contact.CustomerID + "','" + contact.ContactName + "','" + contact.ContactTitle + "','" + contact.ContactType + "' ,'" + contact.Phone + "','" + contact.Cell + "','" + contact.Fax + "','" + contact.Email + "','" + contact.ContactComment + "','" + contact.Inactive + "')";
                retval = _db.ExecuteNonQuery_IUD(InsertQuery);
            }
            catch (Exception ex)
            {

            }
            return retval;
        }
    }
}