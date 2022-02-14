using Microsoft.AspNetCore.Mvc;
using Fusion.Server.Service;
using Fusion.Server.Service.imp;

using Fusion.Shared.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fusion.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderListController : ControllerBase
    {
        OrderBase db = new ManageOrder();
        // GET: api/<OrderLstController>
        [HttpGet]
        public ResponseModel Get()
        {
            ResponseModel res = new ResponseModel();
            List<Order> lst = new List<Order>();
            lst = db.GetOrderList();
            if (lst.Count > 0)
            {
                res.Status = true;
                res.Result = lst;
            }
            else
                res.Status = false;
            return res;
        }
        
        public ResponseModel GetOrder(string strStatus, string strFilter, string Areaname)
        {
            ResponseModel res = new ResponseModel();
            List<Order> lst = new List<Order>();
            lst = db.GetOrderListByFilter(strStatus, strFilter, Areaname);
            if (lst.Count > 0)
            {
                res.Status = true;
                res.Result = lst;
            }
            else
                res.Status = false;
            return res;
        }
        
        public IEnumerable<SelectItem> GetAllAreas()
        {
            IEnumerable<SelectItem> Area = new List<SelectItem>();

            Area = db.GetAllAreas();         
           
            return Area;
        }

    }
}
