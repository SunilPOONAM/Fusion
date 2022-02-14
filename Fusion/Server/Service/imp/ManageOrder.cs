using Fusion.Server.Helper;
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
    public class ManageOrder : OrderBase
    {
        #region Declaration
        private readonly SqlDataAccess db = new SqlDataAccess();
        private readonly SqlDataAccess db1 = new SqlDataAccess("FleetPlus");
        private readonly SqlDataAccess db2 = new SqlDataAccess("CSAll");
        DataTable dtContainer;
        #endregion
        public override List<Order> GetOrderList()
        {
            List<Order> order = new List<Order>();
            try
            {
                string query = "SELECT OrderID, Description, Status, NAVJobID, CJName AS Customer_Job, AreaName, AreaID FROM vwOrdersArea where AreaName like('%%') and Status = 0";
                dtContainer = db.DataTable_return(query);
                if (dtContainer.Rows.Count > 0)
                {
                    order = GenerateSQL.ConvertToList<Order>(dtContainer);
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return order;
        }

        public override List<Order> GetOrderListByFilter(string strStatus, string strFilter, string Areaname)
        {
            List<Order> order = new List<Order>();
            try
            {
                string cmd = "SELECT OrderID, Description, Status, NAVJobID, CJName as Customer_Job, AreaName, AreaID FROM vwOrdersArea WHERE status=" + strStatus + " ";
                if (!string.IsNullOrEmpty(Areaname.ToString()) && Areaname != "%")
                {
                    var areaID = Areaname;
                    cmd += " AND AreaID = " + areaID;

                }
                if (int.TryParse(strFilter, out int j))
                {
                    cmd += " and OrderID = " + j + "";
                }
                else
                {
                    cmd += "and Description like '%" + strFilter + "%'";
                }
                dtContainer = db.DataTable_return(cmd);
                if (dtContainer.Rows.Count > 0)
                {
                    order = GenerateSQL.ConvertToList<Order>(dtContainer);
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return order;
        }
        public override Order GetOrderInfoById(string orderId)
        {
            Order order = new Order();
            try
            {
                string cmd = "SELECT CONVERT(varchar(10),O.OrderID) as OrderID , O.Description, O.Status, O.Notes, O.TemplateName, O.MeterAddress, O.FieldTicket, O.RetentionPercentage, O.AreaID, O.NAVJobID, O.QuoteNotes, C.CustID, J.JobID,J.Name as JobName, O.ScopeOfWork,C.Name as CustName,C.Address_1 as CustAddress,C.City as CustCity,C.State as CustState,C.Zip as CustZip,J.Address_1 as JobAddress,J.City as JobCity,J.State as JobState,J.Zip as JobZip, C.Name + ' (' + C.CustID + ')' AS Customer, J.Name + ' (' + C.CustID + '-' + J.JobID + ')' AS Job, SUM(OI.ExtValue) AS OrderTotal, O.CreatedBy, O.CreatedOn, O.QuotedBy, O.QuotedOn, O.AwardedBy, O.AwardedOn, O.ClosedBy, O.ClosedOn, O.TruckID, O.Driver1, O.Driver2, O.ScheduledDate, O.CompletedDate, O.StopNumber, O.OpportunityID, C.IsCreditHold, C.DeliveryMethodType, C.DeliveryMethodDesc, C.SignRequirementType, C.PORequirementType, C.PORequirementAmount, C.InvoiceAttachmentType, O.isBRSApproved, O.BRSApprovedBy, O.BRSApprovedDate, J.BillingRequirement1, J.BillingRequirement2, J.BillingRequirement3, J.BillingRequirement4,O.Contract# as Contract FROM Orders AS O INNER JOIN Jobs AS J ON O.JobID = J.JobID INNER JOIN Customers AS C ON J.CustID = C.CustID LEFT OUTER JOIN OrderItems AS OI ON OI.OrderID = O.OrderID WHERE (O.OrderID = " + orderId + ") GROUP BY O.OrderID, O.Description, O.Status, O.Notes, O.TemplateName, O.MeterAddress, O.FieldTicket, C.CustID, J.JobID, C.Name, J.Name, O.CreatedBy, O.CreatedOn, O.QuotedBy, O.QuotedOn, O.AwardedBy, O.AwardedOn, O.ClosedBy, O.ClosedOn, O.RetentionPercentage, O.AreaID, O.NAVJobID, O.QuoteNotes, O.ScopeOfWork, O.TruckID, O.Driver1, O.Driver2, O.ScheduledDate, O.CompletedDate, O.StopNumber, O.OpportunityID, C.IsCreditHold, C.DeliveryMethodType, C.DeliveryMethodDesc, C.SignRequirementType, C.PORequirementType, C.PORequirementAmount, C.InvoiceAttachmentType, O.isBRSApproved, O.BRSApprovedBy, O.BRSApprovedDate, J.BillingRequirement1, J.BillingRequirement2, J.BillingRequirement3, J.BillingRequirement4,C.Address_1,C.City,C.State,C.Zip,J.Address_1,J.City,J.State,J.Zip,O.Contract#";
                dtContainer = db.DataTable_return(cmd);
                if (dtContainer.Rows.Count > 0)
                {
                    order=GenerateSQL.GetItem<Order>(dtContainer.Rows[0]);
                    order.TaxRate = GetTax(orderId);                    
                    
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return order;
        }

        public override List<SelectItem> GetAllOrderId()
        {
            List<SelectItem> lst = new List<SelectItem>();
            try
            {
                string cmd = "SELECT [OrderID] FROM [Orders]";
                dtContainer = db.DataTable_return(cmd);
                if (dtContainer.Rows.Count > 0)
                {
                    for (int i = 0; i < dtContainer.Rows.Count; i++)
                    {
                        SelectItem sl = new SelectItem();
                        sl.Text = dtContainer.Rows[i]["OrderID"].ToString();
                        sl.Value = dtContainer.Rows[i]["OrderID"].ToString();
                        lst.Add(sl);
                    }
                    //lst = GenerateSQL.ConvertToList<SelectItem>(dtContainer);
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return lst;
        }

        public override List<SelectItem> GetAllSalesReport()
        {
            List<SelectItem> lst = new List<SelectItem>();
            try {
                string cmd = "SELECT FirstName + ' ' + LastName AS Name, EmployeeID FROM AllEmployees AS A WHERE (FirstName IS NOT NULL) AND (LastName IS NOT NULL) AND (isSales = 1) ORDER BY Name";
                DataTable dt6 = db.DataTable_return(cmd);
                if (dt6.Rows.Count > 0)
                {
                    for (int i = 0; i < dt6.Rows.Count; i++)
                    {
                        SelectItem sl = new SelectItem();
                        sl.Text = dt6.Rows[i]["Name"].ToString();
                        sl.Value = dt6.Rows[i]["EmployeeID"].ToString();
                        lst.Add(sl);
                    }
                }
            }
            catch(Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return lst;
        }

        public override List<SelectItem> GetAllDdl(string type, string orderId, string truckId)
        {
            List<SelectItem> lst = new List<SelectItem>();
            switch (type)
            {
                case "Driver":
                    string cmd = " SELECT FirstName +' ' + LastName AS Name, EmployeeID FROM AllEmployees AS A WHERE(FirstName IS NOT NULL) AND(LastName IS NOT NULL) AND(Active = 1) AND(isTPTech = 1) ORDER BY Name";
                    DataTable dt6 = db.DataTable_return(cmd);
                    if (dt6.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt6.Rows.Count; i++)
                        {
                            SelectItem sl = new SelectItem();
                            sl.Text = dt6.Rows[i]["Name"].ToString();
                            sl.Value = dt6.Rows[i]["EmployeeID"].ToString();
                            lst.Add(sl);
                        }
                    }
                    break;
                case "Area":
                    string cmd2 = "  SELECT '' AS AreaID, '' AS AreaName, '' AS DivisionName, 0 AS NextJobNumber UNION SELECT AreaID, AreaName, DivisionName, NextJobNumber FROM Areas";
                    DataTable dt2 = db.DataTable_return(cmd2);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            SelectItem sl = new SelectItem();
                            sl.Text = dt2.Rows[i]["AreaName"].ToString();
                            sl.Value = dt2.Rows[i]["AreaID"].ToString();
                            lst.Add(sl);
                        }
                    }
                    break;
                case "Quick_Add":
                    string cmd3 = "SELECT CodeID ,CodeMemo FROM [tblCodes] where [CodeType] = 'QNOTES'";
                    DataTable dt3 = db2.DataTable_return(cmd3);
                    if (dt3.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            SelectItem sl = new SelectItem();
                            sl.Text = dt3.Rows[i]["CodeMemo"].ToString();
                            sl.Value = dt3.Rows[i]["CodeID"].ToString();
                            lst.Add(sl);
                        }
                    }
                    break;
                case "Codes_Status":
                    string cmd4 = "SELECT CodeType, CodeID, CodeDesc FROM Codes WHERE(CodeType = 'OrderStatusType')";
                    DataTable dt4 = db.DataTable_return(cmd4);
                    if (dt4.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt4.Rows.Count; i++)
                        {
                            SelectItem sl = new SelectItem();
                            sl.Text = dt4.Rows[i]["CodeDesc"].ToString();
                            sl.Value = dt4.Rows[i]["CodeID"].ToString();
                            lst.Add(sl);
                        }
                    }
                    break;
                case "QNotes":
                    string[] arr = new string[] { "Select", "Master Contract/Next Phase", "Call into Office", "Call into Salesperson", "Website Lead", "Lead Generation Team", "Invitation to Bid" };
                    if (arr.Length > 0)
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            SelectItem sl = new SelectItem();
                            sl.Text = arr[i];
                            sl.Value = i.ToString();
                            lst.Add(sl);
                        }
                    }
                    break;

                default:
                    string cmd1 = "SELECT TruckID, Description, AreaID, Location, Make, Year FROM Trucks WHERE(Status <> 'Sold')";
                    DataTable dt1 = db1.DataTable_return(cmd1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            SelectItem sl = new SelectItem();
                            sl.Text = dt1.Rows[i]["TruckID"].ToString();
                            sl.Value = dt1.Rows[i]["Description"].ToString() + " " + dt1.Rows[i]["AreaID"].ToString();
                            lst.Add(sl);
                        }
                    }
                    else
                    {
                        SelectItem sl = new SelectItem();
                        sl.Text = truckId;
                        sl.Value = truckId;
                        lst.Add(sl);
                    }
                    break;
            }
            return lst;
        }
        public string GetTax(string OrderId)
        {
            string tax = "";
            string cmd = "select Case When J.Address_1 is null then C.Address_1 else J.Address_1 END AS Address_1,Case When J.Address_2 is null then C.Address_2 else J.Address_2 END AS Address_2, Case When J.zip is null then C.zip else J.zip END AS zip,Case When J.zip is null then C.city else J.city END AS city, Case When J.zip is null then C.state else J.state END AS state from Orders O INNER JOIN Jobs AS J ON O.[JobID] = J.[JobID] INNER JOIN Customers AS C ON C.[CustID] = J.[CustID] Where O.OrderId=" + OrderId + "";
            DataTable dt = db.DataTable_return(cmd);
            if (dt.Rows.Count > 0)
            {
                string cmd1 = "select * from vwForTaxRate where ZipCode='" + dt.Rows[0]["zip"] + "' and City='" + dt.Rows[0]["city"] + "' and StateCode='" + dt.Rows[0]["state"] + "'";
                DataTable dt1 = db.DataTable_return(cmd1);
                if (dt1.Rows.Count > 0)
                {
                    tax = dt1.Rows[0]["TaxRate"].ToString();
                }
            }
            return tax;
        }

        public override List<OrderItems> GetTask_PartList(string OrderId)
        {
            List<OrderItems> lst = new List<OrderItems>();
            try
            {
                string cmd5 = "SELECT OI.OrderID, OI.linenum, OI.PartID, OI.ItemDesc, OI.Quantity, OI.UnitPrice, OI.Units, OI.ExtValue, G.CodeDesc, OI.GL_Code, OI.Taxable, G.CodeDesc, SUM(ISNULL(InvoiceItems.InvQty, 0)) AS QtyInvoiced FROM OrderItems AS OI INNER JOIN GL_Codes AS G ON G.GLCode = OI.GL_Code LEFT OUTER JOIN InvoiceItems ON OI.OrderID = InvoiceItems.OrderID AND OI.linenum = InvoiceItems.InvoiceItem WHERE (OI.OrderID = " + OrderId + ") GROUP BY OI.OrderID, OI.linenum, OI.PartID, OI.ItemDesc, OI.Quantity, OI.UnitPrice, OI.Units, OI.ExtValue, OI.GL_Code, OI.Taxable, G.CodeDesc";
                dtContainer = db.DataTable_return(cmd5);
                if (dtContainer.Rows.Count > 0)
                {
                    lst = GenerateSQL.ConvertToList<OrderItems>(dtContainer);
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return lst;
        }

        public override List<OrderInvoice> GetTicket_Invoice(string orderId)
        {
            List<OrderInvoice> lst = new List<OrderInvoice>();
            try
            {
                string cmd6 = "SELECT 'P' + CONVERT(nvarchar(10), I.OrderID) + '-' + CONVERT(nvarchar(10), I.InvoiceID) AS 'InvoiceName',Codes.CodeDesc, I.InvoiceDate, I.InvoiceTotal, I.InvDesc,I.InvNotes, I.InvoiceID, I.OrderID FROM Invoices AS I left join Orders as O on I.OrderID = O.OrderID LEFT outer join InvoiceItems AS II ON II.InvoiceID = I.InvoiceID LEFT OUTER JOIN Codes ON I.InvStatus = Codes.CodeID AND Codes.CodeType = 'InvStatusType' WHERE(I.OrderID = " + orderId + ") GROUP BY I.InvoiceID, Codes.CodeDesc, I.InvoiceDate, I.InvoiceTotal, I.InvNotes, I.OrderID, I.InvDesc";
                dtContainer = db.DataTable_return(cmd6);
                if (dtContainer.Rows.Count > 0)
                {
                    lst = GenerateSQL.ConvertToList<OrderInvoice>(dtContainer);
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return lst;
        }
        public override List<JobTimeEntries> GetJobTimeEntries(string orderID, string employId)
        {
            List<JobTimeEntries> lst = new List<JobTimeEntries>();
            try
            {
                string cmd6 = "SELECT RowID, Date, StartTime, EndTime, Hours FROM vwEmployeeJobTimeEntries WHERE EmployeeID = " + employId + " AND OrderID = " + orderID + "";
                dtContainer = db.DataTable_return(cmd6);
                if (dtContainer.Rows.Count > 0)
                {
                    lst = GenerateSQL.ConvertToList<JobTimeEntries>(dtContainer);
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return lst;
        }

        public override IEnumerable<SelectItem> GetAllAreas()
        {
            List<SelectItem> lst = new List<SelectItem>();
            try
            {
                string cmd = "SELECT '%' AS AreaID, 'All Areas' AS AreaName, '' AS DivisionName, 0 AS NextJobNumber, 0 As NavDimension UNION SELECT AreaID, AreaName, DivisionName, NextJobNumber, NavDimension FROM Areas order by AreaName asc";
                dtContainer = db.DataTable_return(cmd);
                if (dtContainer.Rows.Count > 0)
                {
                    for (int i = 0; i < dtContainer.Rows.Count; i++)
                    {
                        SelectItem sl = new SelectItem();
                        sl.Text = dtContainer.Rows[i]["AreaName"].ToString();
                        sl.Value = dtContainer.Rows[i]["AreaID"].ToString();
                        lst.Add(sl);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return lst;
        }
        public override bool UpdateOrderInfo(Order od)
        {
            try
            {
                
                string s="", s1="";
                if (od.Notes != null )
                {
                 s = od.Notes.Replace("'", "''");
                }
                if (od.QuoteNotes != null)
                {
                    s1 = od.QuoteNotes.Replace("'", "''");
                }
                string cmd = "update Orders set JobID = '" + od.JobID + "' ,AreaID='" + od.AreaID + "',Description ='" + od.Description + "' ," + " Notes= '" + s + "' ,Status= " + od.Status + " ,MeterAddress= '" + od.MeterAddress + "' ," + " FieldTicket= '" + od.FieldTicket + "' ,RetentionPercentage= '" + od.RetentionPercentage + "' , NAVJobID= '" + od.NAVJobID + "' ,QuoteNotes='" + s1 + "',ScopeOfWork='" + od.ScopeOfWork + "', TruckID='" + od.TruckID + "'" + ", Driver1='" + od.Driver1 + "'" + ", Driver2='" + od.Driver1 + "'" + ", ScheduledDate='" + Convert.ToDateTime(od.ScheduledDate).ToString("MM/dd/yy HH:mm:ss") + "'" + ", CompletedDate='" + Convert.ToDateTime(od.CompletedDate).ToString("MM/dd/yy HH:mm:ss") + "'" + ", StopNumber='" + od.StopNumber + "'" + ", OpportunityID ='" + od.OpportunityID + "'" + ", isBRSApproved = '" + od.isBRSApproved + "', BRSApprovedBy = '" + od.BRSApprovedBy + "', BRSApprovedDate = '" + od.BRSApprovedDate + "', Contract# = '" + od.Contract + "' WHERE OrderID = '" + od.OrderID + "'";
                string cmd2 = cmd.Replace("01-01-01 00:00:00", "");
                int n = db.ExecuteNonQuery_IUD(cmd2);
                if (n > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return false;
        }
        public override bool UpdateQuotedOn(Order od)
        {
            try
            {
                string cmd = "UPDATE Orders SET QuotedBy=" + od.QuotedBy + ",QuotedOn='" + Convert.ToDateTime(od.QuotedOn).ToString("MM/dd/yy HH:mm:ss") + "' WHERE OrderID=" + od.OrderID + "";
                int n = db.ExecuteNonQuery_IUD(cmd);
                if (n > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return false;
        }
        public override bool UpdateClosedOn(Order od)
        {
            try
            {
                string cmd = "UPDATE Orders SET ClosedBy=" + od.ClosedBy + ",ClosedOn='" + Convert.ToDateTime(od.ClosedOn).ToString("MM/dd/yy HH:mm:ss") + "'  WHERE OrderID=" + od.OrderID + "";
                int n = db.ExecuteNonQuery_IUD(cmd);
                if (n > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogManager.InsertLog(LogTypeEnum.DatabaseOprationError, ex.Message, ex);
            }
            return false;
        }

    }
}
