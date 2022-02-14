using Microsoft.AspNetCore.Mvc;
using Fusion.Server.Helper;
using Fusion.Server.Service.imp;
using Fusion.Server.Service;
using Fusion.Shared.Models;
using System.Collections.Generic;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fusion.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderInfoController : ControllerBase
    {
        SqlDataAccess db = new SqlDataAccess();
        OrderBase orderbase = new ManageOrder();
        // GET: api/<OrderInfo>
        [HttpGet]
        public Order Get(string OrderId)
        {
            var order = orderbase.GetOrderInfoById(OrderId);           
            return order;
        }
        
        public IEnumerable<SelectItem> GetOrderIdsList()
        {           
            List<SelectItem> lst = orderbase.GetAllOrderId();
            return lst.ToArray();
        }

        
        public IEnumerable<SelectItem> GetSalesRepList(string type, string OrderId)
        {
            List<SelectItem> lst = orderbase.GetAllSalesReport();
            return lst.ToArray();
        }
       
        public IEnumerable<SelectItem> GetAllDDlList(string type, string OrderId, string TruckId)
        {
            List<SelectItem> lst = orderbase.GetAllDdl(type, OrderId, TruckId);
            return lst.ToArray();
        }

        public IEnumerable<OrderInvoice> GetOrderInvoices(string OrderId)
        {
            List<OrderInvoice> order_Invoice = orderbase.GetTicket_Invoice(OrderId);
            return order_Invoice.ToArray();
        }

        public IEnumerable<OrderItems> GetOrderItems(string OrderId)
        {
            List<OrderItems> orderitems_tbl = orderbase.GetTask_PartList(OrderId);
            return orderitems_tbl.ToArray();
        }

        public IEnumerable<JobTimeEntries> GetJobTimeEntries(string OrderID, string EmployId)
        {
            List<JobTimeEntries> order_Invoice2 = orderbase.GetJobTimeEntries(OrderID, EmployId);
            return order_Invoice2.ToArray();
        }
        [HttpPost]
        public ResponseModel Post([FromBody] Order od)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                bool n = orderbase.UpdateOrderInfo(od);
                if (n)
                {
                    res.Status = true;
                    res.Message = "Record updated successfully";
                }
                else
                {
                    res.Status = false;
                    res.Message = "Error in updating record";
                }
            }
            catch (Exception)
            {
                res.Status = false;
                res.Message = "Error in updating record";
            }
            return res;
        }

        public ResponseModel QuotedOn([FromBody] Order od)
        {
            bool n = orderbase.UpdateQuotedOn(od);
            ResponseModel res = new ResponseModel();
            return res;
        }
        [HttpPost]
        public ResponseModel ClosedOn([FromBody] Order od)
        {
            bool n = orderbase.UpdateClosedOn(od);
            ResponseModel res = new ResponseModel(); 
            return res;
        }
    }
}
