using Microsoft.AspNetCore.Mvc;
using Fusion.Server.Service.imp;
using Fusion.Server.Service;
using Fusion.Shared.Models;
using Fusion.Shared;
using System.Collections.Generic;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fusion.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillToCustomerController : ControllerBase
    {
        OpportunityBase mop = new ManageOpportunity();
        DiscussionBase mds = new ManageDiscussions();
        AssignmentBase mas = new ManageAssignments();
        CustomerBase mnc = new ManageCustomer();
        EmployeeBase me = new ManageEmployee();
        CodesBase mc = new ManageCodes();

        [HttpGet]
        public Customer Get(string Id)
        {
            var cust = mnc.GetCustomerById(Id);           
            return cust;
        }
        [HttpGet]
        public List<Contact> GetContract(string Id)
        {
            var contact = mnc.GetCustomerContactInfoByCustomerID(Id);
            return contact;
        }
        [HttpGet]
        public Opportunity GetOpportunity(string Id)
        {
            var opp = mop.GetOpportunity(Id);
            return opp;
        }

        [HttpGet]
        public List<Discussion> LoadCustomerDiscussions(string Id)
        {
            var list = mds.GetDiscussions(Constants.Customer, Id);
            return list;
        }

        [HttpGet]
        public List<vwAssignment> LoadCustomerAssignments(string Id)
        {
            var list = mas.GetAssignmentsBytblName(Id, Constants.Customer);
            return list;
        }

        [HttpGet]
        public List<vwAssignment> LoadOppAssignments(string Id)
        {
            var list = mnc.GetOpenOpportunityByCustomerId(Id);
            return list;
        }

        [HttpGet]
        public List<Opportunity> LoadCustomerOpportunities(string Id)
        {
            var list = mnc.GetCustomerOpportunitiesList(Id);
            return list;
        }

        [HttpGet]
        public List<Agreement> LoadCustomerAgreements(string Id)
        {
            var list = mnc.GetAgreementsByCustomerId(Id);
            return list;
        }

        [HttpGet]
        public List<tblCode> AsgnStatusList()
        {
            var list = mc.GetCodesByType("AsgnStatus");
            return list;
        }

        [HttpGet]
        public List<Employee> EmployeesList()
        {
            var list = me.GetEmployees();
            return list;
        }

        [HttpGet]
        public List<MyParameter> LoadDeliveryMethods()
        {
            List<MyParameter> list = new List<MyParameter>();
            var deliveryMethods = Enum.GetValues(typeof(DeliveryMethods));
            foreach (var method in deliveryMethods)
            {
                MyParameter dm = new MyParameter(((DeliveryMethods)method).GetDescription(), method);
                list.Add(dm);
            }
            return list;
        }

        [HttpGet]
        public List<MyParameter> LoadSignMethods()
        {
            List<MyParameter> list = new List<MyParameter>();
            var signMethods = Enum.GetValues(typeof(SignMethods));
            foreach (var method in signMethods)
            {
                MyParameter sm = new MyParameter(((SignMethods)method).GetDescription(), method);
                list.Add(sm);
            }
            return list;
        }

        [HttpGet]
        public List<MyParameter> LoadPOMethods()
        {
            List<MyParameter> list = new List<MyParameter>();
            var poMethods = Enum.GetValues(typeof(POMethods));
            foreach (var method in poMethods)
            {
                MyParameter pm = new MyParameter(((POMethods)method).GetDescription(), method);
                list.Add(pm);
            }
            return list;
        }

        [HttpGet]
        public List<MyParameter> LoadAttachmentMethods()
        {
            List<MyParameter> list = new List<MyParameter>();
            var attachmentMethods = Enum.GetValues(typeof(AttachMethods));
            foreach (var method in attachmentMethods)
            {
                MyParameter am = new MyParameter(((AttachMethods)method).GetDescription(), method);
                list.Add(am);
            }
            return list;
        }
        
        [HttpPost]
        public bool AddCustomer([FromBody] Customer cust)
        {
            bool res = false;
            try
            {
                res = mnc.AddCustomer(cust);
            }
            catch (Exception) { }
            return res;
        }
        public bool AddContract([FromBody] Contact cust)
        {
            int res =0;
            try
            {
                res = mnc.SaveContactInfo(cust);
            }
            catch (Exception) { }
            if (res > 0)
                return true;
            else
                return false;
        }
        [HttpPost]
        public bool UpdateCustomer([FromBody] Customer cust)
        {
            bool res = false;
            try
            {
                res = mnc.UpdateCustomer(cust);
            }
            catch (Exception) { }
            return res;
        }

        [HttpPost]
        public bool UpdateOpportunity([FromBody] Opportunity opp)
        {
            bool res = false;
            try
            {
                res = mop.UpdateOpportunity(opp);
            }
            catch (Exception) { }
            return res;
        }

        [HttpPost]
        public bool AddCustAssignment([FromBody] Assignment asgn)
        {
            bool result = mas.AddAssignment(asgn);
            return result;
        }

        [HttpPost]
        public bool UpdateCustAssignment([FromBody] Assignment asgn)
        {
            bool result = mas.UpdateAssignment(asgn);
            return result;
        }

        [HttpPost]
        public bool AddCustDiscussion([FromBody] Discussion dis)
        {
            bool result = mds.AddDiscussion(dis);
            return result;
        }

        [HttpPost]
        public bool UpdateCustDiscussion([FromBody] Discussion dis)
        {
            bool result = mds.UpdateDiscussion(dis);
            return result;
        }

    }
}
