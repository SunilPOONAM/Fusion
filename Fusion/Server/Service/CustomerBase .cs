using Fusion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fusion.Server.Service
{
    public abstract class CustomerBase 
    {
        public abstract Customer GetCustomerById(string id);
        public abstract List<Customer> GetCustomerList();
        public abstract List<Customer> GetSearchedCustomers(string searchstr);
        public abstract bool AddCustomer(Customer cust);
        public abstract bool UpdateCustomer(Customer cust);
        public abstract List<vwAssignment> GetOpenOpportunityByCustomerId(string customerId);
        public abstract List<Opportunity> GetCustomerOpportunitiesList(string CustomerId);
        public abstract List<Agreement> GetAgreementsByCustomerId(string CustomerId);
        public abstract List<Contact> GetCustomerContactInfoByCustomerID(string customerId);
        public abstract int SaveContactInfo(Contact contact);
    }
}