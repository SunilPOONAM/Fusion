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
    public class ManageOpportunity : OpportunityBase
    {
        #region Declaration
        private readonly SqlDataAccess _db = new SqlDataAccess();
        DataTable dtContainer;
        #endregion

        public override Opportunity GetOpportunity(string id)
        {
            Opportunity retval = new Opportunity();
            dtContainer = new DataTable();
            int oppID = Convert.ToInt32(id);
            try
            {
                string query = "Select * FROM Opportunities Where OppID=" + oppID + "";
                dtContainer = _db.DataTable_return(query);
                if (dtContainer.Rows.Count > 0)
                {
                    retval = GenerateSQL.GetItem<Opportunity>(dtContainer.Rows[0]);
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
        public override int AddOpportunity(Opportunity opp)
        {
            bool retval = false;
            int result = 0;
            try
            {
                opp.SetPropertyValues();

                string EntryDate = opp.EntryDate != null ? Convert.ToDateTime(opp.EntryDate).ToString("MM-dd-yyyy") : null;
                string InsertQuery = "insert into Opportunities(CustomerID,ContactName,Nickname,Description,EnteredBy,Value,Status,OppType,ContactEmail,ContactPhone," +
                    " Stage,Probability,UnitQty,EntryDate,CustomerName,CustomerAddr1,CustomerAddr2,CustomerCity,CustomerState,CustomerZip," +
                    " CustomerPhone,Industry,AgreementID,ServiceCallID,OpportunityOwner,OpportunitySource) values ('" + opp.CustomerID + "','" + opp.ContactName + "','" + opp.Nickname + "','" + opp.Description + "'" +
                    " ," + opp.EnteredBy + "," + opp.Value + ",'" + opp.Status + "','" + opp.OppType + "','" + opp.ContactEmail + "','" + opp.ContactPhone + "','" + opp.Stage + "'" +
                    " ,'" + opp.Probability + "'," + opp.UnitQty + ",DATEADD(minute,-90,GETDATE()) ,'" + opp.CustomerName + "','" + opp.CustomerAddr1 + "'" +
                    ",'" + opp.CustomerAddr2 + "','" + opp.CustomerCity + "','" + opp.CustomerState + "','" + opp.CustomerZip + "','" + opp.CustomerPhone + "','" + opp.Industry + "'," + opp.AgreementID + "" +
                    "," + opp.ServiceCallID + ",'" + opp.OpportunityOwner + "','" + opp.OpportunitySource + "');SELECT SCOPE_IDENTITY();";
                var i = _db.DataTable_return(InsertQuery);

                result = Convert.ToInt32(i.Rows[0][0]);

            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public override int GetRecentOpportunity()
        {
            dtContainer = new DataTable();
            int retval = 0;
            try
            {
                string query = "SELECT TOP 1 OppID FROM Opportunities ORDER BY OppID DESC";
                dtContainer = _db.DataTable_return(query);
                if (dtContainer.Rows.Count > 0)
                {
                    retval = Convert.ToInt32(dtContainer.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {

            }
            return retval;
        }
        public override bool UpdateOpportunity(Opportunity opp)
        {
            int result = 0;
            List<Opportunity> custList = new List<Opportunity>();
            try
            {
                opp.SetPropertyValues();

                string probability = opp.Probability == null ? "0" : opp.Probability.ToString();
                string query = "UPDATE [Opportunities] SET[CustomerID] = '" + opp.CustomerID + "',[ContactName] = '" + opp.ContactName + "',[Nickname] = '" + opp.Nickname + "'," +
                    " [Description] = '" + opp.Description + "',[Value] = " + opp.Value + ",[Status] = '" + opp.Status + "',[OppType] = '" + opp.OppType + "'," +
                    " [ContactEmail] = '" + opp.ContactEmail + "',[ContactPhone] = '" + opp.ContactPhone + "',[Stage] = '" + opp.Stage + "',[Probability] = " + probability + "," +
                    " [UnitQty] = " + opp.UnitQty + ",[CustomerName] = '" + opp.CustomerName + "'," +
                    " [CustomerAddr1] = '" + opp.CustomerAddr1 + "',[CustomerAddr2] = '" + opp.CustomerAddr2 + "',[CustomerCity] = '" + opp.CustomerCity + "',[CustomerState] = '" + opp.CustomerState + "'," +
                    " [CustomerZip] = '" + opp.CustomerZip + "',[CustomerPhone] = '" + opp.CustomerPhone + "',[Industry] = '" + opp.Industry + "',[AgreementID] = " + opp.AgreementID + "," +
                    " [ServiceCallID] = " + opp.ServiceCallID + ",[OpportunityOwner] = '" + opp.OpportunityOwner + "',[OpportunitySource] = '" + opp.OpportunitySource + "' WHERE OppID = " + opp.OppID + "";
                result = _db.ExecuteNonQuery_IUD(query);

            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public override bool CloseOpportunity(Opportunity opp)
        {
            int result = 0;
            try
            {
                opp.SetPropertyValues();

                string ClosedDate = opp.ClosedDate != null ? "'" + Convert.ToDateTime(opp.ClosedDate).ToString("MM-dd-yyyy") + "'" : "NULL";
                string query = "UPDATE [Opportunities] SET [Status] = '" + opp.Status + "',[Stage] = '" + opp.Stage + "',[ClosedDate] = " + ClosedDate + " WHERE OppID = " + opp.OppID + "";
                result = _db.ExecuteNonQuery_IUD(query);
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public override bool PromoteOpportunity(Opportunity opp)
        {
            int result = 0;
            try
            {
                opp.SetPropertyValues();

                string PromotedDate = opp.PromoteDate != null ? "'" + Convert.ToDateTime(opp.PromoteDate).ToString("MM-dd-yyyy") + "'" : "NULL";
                string query = "UPDATE [Opportunities] SET [Status] = '" + opp.Status + "',[AgreementID] = '" + opp.AgreementID + "',[PromotedDate] = " + PromotedDate + ", [ServiceCallID]=NULL WHERE OppID = " + opp.OppID + "";
                result = _db.ExecuteNonQuery_IUD(query);
            }
            catch (Exception ex)
            {

            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public override List<Opportunity> GetAllOpportunity()
        {
            List<Opportunity> retval = new List<Opportunity>();
            dtContainer = new DataTable();
            int result = 0;
            try
            {
                string query = "Select * from Opportunities";
                dtContainer = _db.DataTable_return(query);
                retval = GenerateSQL.ConvertToList<Opportunity>(dtContainer);
            }
            catch (Exception ex)
            {
            }
            return retval;
        }

    }
}