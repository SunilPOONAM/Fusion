using Fusion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fusion.Server.Service
{
    public abstract class OrderBase
    {
        public abstract List<Order> GetOrderList();

        public abstract List<Order> GetOrderListByFilter(string strStatus, string strFilter, string Areaname);

        public abstract IEnumerable<SelectItem> GetAllAreas();
        public abstract Order GetOrderInfoById(string orderId);

        public abstract List<SelectItem> GetAllOrderId();

        public abstract List<SelectItem> GetAllSalesReport();
        public abstract List<SelectItem> GetAllDdl(string type, string orderId, string truckId);
        public abstract List<OrderItems> GetTask_PartList(string OrderId);
        public abstract List<OrderInvoice> GetTicket_Invoice(string orderId);
        public abstract List<JobTimeEntries> GetJobTimeEntries(string orderID, string employId);
        public abstract bool UpdateOrderInfo(Order od);
        public abstract bool UpdateQuotedOn(Order od);
        public abstract bool UpdateClosedOn(Order od);
    }
}
