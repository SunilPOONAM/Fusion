using Fusion.Client.Components;
using Fusion.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;


namespace Fusion.Client.Pages
{
    public partial class BillToCustomer : ComponentBase
    {
        HttpClient http = new HttpClient();
        private Employee employee = new Employee();
        private Customer cust = new Customer();
        private Contact contact = new();
        private List<Contact> contacts;
        private string loader;
        private ResponseModel res;
        #region Declarations
        public string OppID { get; set; }
        public bool IsCheckedShowExpiredPO { get; set; } = false;
        public bool IsProspect { get; set; }
        public bool IsNewRecord { get; set; }
        public bool IsNewCustomer { get; set; }
        public string MasterCustomerName { get; set; }
        public string EnteredByName { get; set; }
        public DiscussionDialog DiscussionDialog { get; set; }
        public AssignmentDialog AssignmentDialog { get; set; }
        public string SitesCount { get; set; }
        public Opportunity Opp { get; set; }
        public virtual List<Employee> SalesPeopleList { get; set; }
        public virtual List<Employee> AccountMgrList { get; set; }
        #endregion

        public async Task HandleValidSubmit()
        {
            if (contact.ContactName != null)
            {
                StringValues Id;
                var url = MyNavigationManager.ToAbsoluteUri(MyNavigationManager.Uri);
                QueryHelpers.ParseQuery(url.Query).TryGetValue("Id", out Id);
                contact.CustomerID = Id;
                var jsonData = JsonSerializer.Serialize(contact);
                var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var respons = await http.PostAsync("/api/BillToCustomer/AddContract", contentData);
                GetContact();
            }
        }
        private async Task HandleOnChange<TObject, TValue>(ChangeEventArgs uIChangeEventArgs, TObject target, Expression<Func<TObject, TValue>> propertyGetter)
        {
            if (uIChangeEventArgs?.Value != null)
            {
                var expression = (MemberExpression)propertyGetter.Body;
                var property = (PropertyInfo)expression.Member;

                if (property.PropertyType == typeof(Nullable<Int32>))
                {
                    property.SetValue(target, uIChangeEventArgs.Value.ToString() == string.Empty ? (int?)null : Convert.ToInt32(uIChangeEventArgs.Value));
                }
                else if (property.PropertyType == typeof(Nullable<Decimal>))
                {
                    property.SetValue(target, uIChangeEventArgs.Value.ToString() == string.Empty ? (decimal?)null : Convert.ToDecimal(uIChangeEventArgs.Value));
                }
                else if (property.PropertyType == typeof(Nullable<Double>))
                {
                    property.SetValue(target, uIChangeEventArgs.Value.ToString() == string.Empty ? (double?)null : Convert.ToDouble(uIChangeEventArgs.Value));
                }
                else
                {
                    property.SetValue(target, uIChangeEventArgs.Value);
                }

                await SaveData();
            }
        }

        private async Task SaveData()
        {
}
private async Task AddContact1()
        {
            if (contact.ContactName != null)
            {
                StringValues Id;
                var url = MyNavigationManager.ToAbsoluteUri(MyNavigationManager.Uri);
                QueryHelpers.ParseQuery(url.Query).TryGetValue("Id", out Id);
                contact.CustomerID = Id;
                var jsonData = JsonSerializer.Serialize(contact);
                var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var respons = await http.PostAsync("/api/BillToCustomer/AddContract", contentData);
                GetContact();
            }
        }
        
        private async Task GetContact()
        {
            contacts = await http.GetFromJsonAsync<List<Contact>>("/api/BillToCustomer/GetContract?Id=" + cust.CustomerID);
            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            StringValues Id, OppId;
            loader = "<div class='loader'><img src='./Content/images/Vp3R.gif'/></div>";
            http.BaseAddress = new Uri(MyNavigationManager.Uri);
            var url = MyNavigationManager.ToAbsoluteUri(MyNavigationManager.Uri);
            employee = await sessionStorage.GetItemAsync<Employee>("Employee");

            QueryHelpers.ParseQuery(url.Query).TryGetValue("Id", out Id);
            QueryHelpers.ParseQuery(url.Query).TryGetValue("OppID", out OppId);
            await LoadCustomerPage(Id, OppId);
            loader = "";
        }

        private async Task LoadCustomerPage(string Id, string oppID)
        {
            if (string.IsNullOrEmpty(Id))
            {
                IsNewCustomer = true;
                cust.CustomerStatus = "PROSPECT";
                cust.EntryDate = DateTime.Now;
                cust.EnteredBy = employee.EmployeeID;

                if (!string.IsNullOrEmpty(oppID))
                {
                    Opp = await http.GetFromJsonAsync<Opportunity>("./api/BillToCustomer/GetOpportunity?Id=" + oppID);
                    cust.CustomerID = Opp.CustomerID ?? "";
                    cust.CustomerName = Opp.CustomerName ?? "";
                    cust.Nickname = Opp.Nickname ?? "";
                    cust.Address1 = Opp.CustomerAddr1 ?? "";
                    cust.Address2 = Opp.CustomerAddr2 ?? "";
                    cust.City = Opp.CustomerCity ?? "";
                    cust.State = Opp.CustomerState ?? "";
                    cust.Zip = Opp.CustomerZip ?? "";
                }
            }
            else
            {
                cust = await http.GetFromJsonAsync<Customer>("./api/BillToCustomer/Get?Id=" + Id);
            }

            OppID = oppID;

            List<Employee> employees = await http.GetFromJsonAsync<List<Employee>>("./api/BillToCustomer/EmployeesList");
            SalesPeopleList = employees.Where(x => x.Role == "Sales").ToList();
            AccountMgrList = employees;

            IsProspect = cust.CustomerStatus.ToUpper().Trim() == "PROSPECT";

            if (cust.EnteredBy.GetValueOrDefault() > 0)
            {
                int res = cust.EnteredBy.GetValueOrDefault();
                Employee emp = employees.Where(x => x.EmployeeID == res).FirstOrDefault();

                if (emp != null)
                {
                    EnteredByName = emp.FirstName + " " + emp.LastName;
                }
            }

            if (cust?.Zip != null && cust.Zip.Length < 9 && cust.Zip.Contains("-"))
            {
                cust.Zip.Replace("-", "");
            }

        }

    }
}
