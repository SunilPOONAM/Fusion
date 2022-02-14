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
    public class OpportunityInfoController : ControllerBase
    {
        OpportunityBase mop = new ManageOpportunity();
        DiscussionBase mds = new ManageDiscussions();
        AssignmentBase mas = new ManageAssignments();
        CustomerBase mnc = new ManageCustomer();
        EmployeeBase me = new ManageEmployee();
        CodesBase mc = new ManageCodes();

        [HttpGet]
        public Opportunity Get(string Id)
        {
            var opp = mop.GetOpportunity(Id);           
            return opp;
        }

        [HttpGet]
        public List<Discussion> LoadOppDiscussions(string Id)
        {
            var list = mds.GetDiscussions(Constants.Opportunity, Id);
            return list;
        }

        [HttpGet]
        public List<vwAssignment> LoadOppAssignments(string Id)
        {
            var list = mas.GetAssignmentsBytblName(Id, Constants.Opportunity);
            return list;
        }

        [HttpGet]
        public List<string> OpportunitySource()
        {
            List<string> oppsource = new List<string> { "Import", "Advertisement", "Cold Call", "Previous Customer", "External Referral", "Trade Show", "Web Search" }; ;
            return oppsource;
        }

        [HttpGet]
        public List<tblCode> AsgnStatusList()
        {
            var list = mc.GetCodesByType("AsgnStatus");
            return list;
        }

        [HttpGet]
        public List<tblCode> IndustryList()
        {
            var list = mc.GetCodesByType("INDUSTRY");
            return list;
        }

        [HttpGet]
        public List<tblCode> StageList()
        {
            var list = mc.GetCodesByType("OPPSTAGE");
            return list;
        }

        [HttpGet]
        public List<tblCode> TypeList()
        {
            var list = mc.GetCodesByType("OPPTYPE");
            return list;
        }

        [HttpGet]
        public List<Employee> EmployeesList()
        {
            var list = me.GetEmployees();
            return list;
        }

        [HttpPost]
        public int SaveData([FromBody] Opportunity opp)
        {
            int id = 0;
            try
            {
                if (opp.OppID == 0)
                {
                    id = mop.AddOpportunity(opp);
                }
                else
                {
                    bool n = mop.UpdateOpportunity(opp);

                    if (n) { id = opp.OppID; }
                }
                
            }
            catch (Exception) { }
            return id;
        }

        [HttpPost]
        public bool AddOppAssignment([FromBody] Assignment asgn)
        {
            bool result = mas.AddAssignment(asgn);
            return result;
        }

        [HttpPost]
        public bool UpdateOppAssignment([FromBody] Assignment asgn)
        {
            bool result = mas.UpdateAssignment(asgn);
            return result;
        }

        [HttpGet]
        public Discussion GetDiscussion(string id)
        {
            return mds.GetDiscussion(id);
        }

        [HttpPost]
        public bool AddOppDiscussion([FromBody] Discussion dis)
        {
            bool result = mds.AddDiscussion(dis);
            return result;
        }

        [HttpPost]
        public bool UpdateOppDiscussion([FromBody] Discussion dis)
        {
            bool result = mds.UpdateDiscussion(dis);
            return result;
        }

        [HttpGet]
        public List<Customer> SearchCustomers(string SearchText)
        {
            var list = mnc.GetSearchedCustomers(SearchText);
            return list;
        }

        [HttpPost]
        public bool CloseOpportunity([FromBody] Opportunity opp)
        {
            var result = mop.CloseOpportunity(opp);
            return result;
        }

        //public ResponseModel QuotedOn([FromBody] Opportunity opp)
        //{
        //    bool n = oppbase.UpdateOpportunity(opp);
        //    ResponseModel res = new ResponseModel();
        //    return res;
        //}

    }
}
