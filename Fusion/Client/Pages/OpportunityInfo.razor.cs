using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Fusion.Shared;
using Fusion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq.Expressions;
using System.Reflection;
using Fusion.Client.Shared;
using Fusion.Client.Components;

namespace Fusion.Client.Pages
{
    public partial class OpportunityInfo : ComponentBase
    {
        HttpClient http = new HttpClient();
        private Employee employee = new Employee();
        private Opportunity opp = new Opportunity();
        private string loader;

        #region Declarations
        public bool ShowCustomerSearch { get; set; }
        public string SearchText { get; set; }
        public List<Customer> SearchedCustomersList { get; set; }
        public DiscussionDialog DiscussionDialog { get; set; }
        public AssignmentDialog AssignmentDialog { get; set; }
        public string CloseOppTitle { get; set; }
        public bool ShowCloseOppModal { get; set; }
        public bool CloseRevisitVisible { get; set; }
        public Nullable<DateTime> RevisitDate { get; set; }
        public bool IsPromotable { get; set; }
        public bool IsProspect { get; set; }
        public string CustomerPrompt { get; set; }
        public string BillToPrompt { get; set; }
        public string AgreementPromtedButtonText { get; set; }
        public string WorkOrderPromtedButtonText { get; set; }
        public virtual List<Discussion> OppDiscussionlist { get; set; }
        public virtual List<vwAssignment> OppAssignmentlist { get; set; }
        public virtual List<Employee> EmployeesList { get; set; }
        public virtual List<tblCode> IndustryList { get; set; }
        public virtual List<tblCode> StageList { get; set; }
        public virtual List<tblCode> TypeList { get; set; }
        public virtual List<string> OpportunitySource { get; set; }
        #endregion

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
            var result = await http.PostAsJsonAsync("./api/OpportunityInfo/SaveData?", opp);

            if (result.IsSuccessStatusCode)
            {
                int id = result.Content.ReadFromJsonAsync<int>().Result;
                
                opp.OppID = id > 0 ? id : opp.OppID;
                
                if (opp.OppID > 0)
                {
                    if (OppAssignmentlist == null || OppAssignmentlist.Count == 0)
                    {
                        vwAssignment defaultAssn = new vwAssignment
                        {
                            AssignedObjectType = Constants.Opportunity,
                            EmployeeID = employee.EmployeeID,
                            FollowUp = DateTime.Now,
                            ObjectID = opp.OppID,
                            Responsibility = "Entered by " + employee.FirstName + " " + employee.LastName,
                            Role = String.Empty
                        };

                        await http.PostAsJsonAsync("./api/OpportunityInfo/AddOppAssignment?", defaultAssn);

                        OppAssignmentlist = await http.GetFromJsonAsync<List<vwAssignment>>("api/OpportunityInfo/LoadOppAssignments?Id=" + opp.OppID);
                        await JS.InvokeAsync<string>("LoadSimpleDataTable", "#tblAssignments");
                    }
                }
            }

            
        } 

        protected override async Task OnInitializedAsync()
        {
            StringValues Id;
            loader = "<div class='loader'><img src='./Content/images/Vp3R.gif'/></div>";
            http.BaseAddress = new Uri(MyNavigationManager.Uri);
            var url = MyNavigationManager.ToAbsoluteUri(MyNavigationManager.Uri);
            employee = await sessionStorage.GetItemAsync<Employee>("Employee");

            QueryHelpers.ParseQuery(url.Query).TryGetValue("Id", out Id);
            
            await LoadOpportunity(Id);
            await JS.InvokeAsync<string>("LoadSimpleDataTable", "#tblNotes");
            await JS.InvokeAsync<string>("LoadSimpleDataTable", "#tblAssignments");

            loader = "";
        }

        private async Task LoadOpportunity(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                opp = await http.GetFromJsonAsync<Opportunity>("./api/OpportunityInfo/Get?Id=" + Id);
            }
            else
            {
                opp = new Opportunity();
                opp.Status = "OPEN";
                opp.OppType = "OppType1";
                opp.Stage = "Stage1";
                opp.EnteredBy = employee.EmployeeID;
            }

            IsProspect = string.IsNullOrEmpty(opp.CustomerID);

            if (IsProspect)
            {
                CustomerPrompt = "Prospect Name";
                BillToPrompt = "Select Bill-To";
            }
            else
            {
                CustomerPrompt = "Bill-To Name";
                BillToPrompt = "Clear Bill-To";
            }

            bool flag = true;

            if (opp.ServiceCallID.NullOrZero() && !opp.AgreementID.NullOrZero())
            {
                AgreementPromtedButtonText = Constants.OppAgmtPromoted + "\n" + opp.AgreementID;
                WorkOrderPromtedButtonText = Constants.OppPromoteToWorkOrder;
                flag = false;
            }
            else if (!opp.ServiceCallID.NullOrZero() && opp.AgreementID.NullOrZero())
            {
                WorkOrderPromtedButtonText = Constants.OppWorkOrderPromoted + "\n" + opp.ServiceCallID;
                AgreementPromtedButtonText = Constants.OppPromoteToAgmt;
                flag = false;
            }
            else
            {
                WorkOrderPromtedButtonText = Constants.OppPromoteToWorkOrder;
                AgreementPromtedButtonText = Constants.OppPromoteToAgmt;
            }

            if (opp.Status == "Close" || opp.Status == "CLOSEDREVISIT")
            {
                flag = false;
            }

            IsPromotable = flag;

            OppDiscussionlist = await http.GetFromJsonAsync<List<Discussion>>("./api/OpportunityInfo/LoadOppDiscussions?Id=" + Id);
            OppAssignmentlist = await http.GetFromJsonAsync<List<vwAssignment>>("./api/OpportunityInfo/LoadOppAssignments?Id=" + Id);

            EmployeesList = await http.GetFromJsonAsync<List<Employee>>("./api/OpportunityInfo/EmployeesList");
            OpportunitySource = await http.GetFromJsonAsync<List<string>>("./api/OpportunityInfo/OpportunitySource");
            IndustryList = await http.GetFromJsonAsync<List<tblCode>>("./api/OpportunityInfo/IndustryList");
            StageList = await http.GetFromJsonAsync<List<tblCode>>("./api/OpportunityInfo/StageList");
            TypeList = await http.GetFromJsonAsync<List<tblCode>>("./api/OpportunityInfo/TypeList");
        }

        public void OpenBillToCustomer()
        {
            MyNavigationManager.NavigateTo("./BillToCustomer?Id=" + opp.CustomerID + "&OppID=" + opp.OppID);
        }

        public void OpenCloseOppModal(string title)
        {
            RevisitDate = null;
            CloseOppTitle = title;
            CloseRevisitVisible = title == "Close (Revisit)";
            ShowCloseOppModal = true;
            StateHasChanged();
        }

        public void CloseModal()
        {
            RevisitDate = null;
            ShowCloseOppModal = false;
            StateHasChanged();
        }

        public async void CloseOpportunity()
        {
            if (CloseOppTitle == "Close (Revisit)")
            {
                opp.Status = "CLOSEDREVISIT";
                opp.Stage = "CLOSEDREVISIT";
            }
            else
            {
                opp.Status = "Closed";
                opp.Stage = "Closed";
            }

            opp.ClosedDate = DateTime.Now;

            var result = await http.PostAsJsonAsync("./api/OpportunityInfo/CloseOpportunity?", opp);

            if (result.IsSuccessStatusCode)
            {
                var updated = result.Content.ReadFromJsonAsync<bool>().Result;

                if (updated)
                {
                    foreach (var assn in OppAssignmentlist)
                    {
                        Assignment asgn = new()
                        {
                            AssignID = assn.AssignID,
                            AssignedObjectType = assn.AssignedObjectType,
                            ObjectID = assn.ObjectID,
                            EmployeeID = assn.EmployeeID,
                            Responsibility = assn.Responsibility,
                            IsPrimary = assn.IsPrimary,
                            Role = assn.Role
                        };

                        switch (opp.Status)
                        {
                            case "CLOSEDREVISIT":
                                asgn.FollowUp = Convert.ToDateTime(RevisitDate);
                                break;
                            case "Closed":
                                asgn.FollowUp = null;
                                asgn.StatusCode = "5";
                                break;
                        }

                        await http.PostAsJsonAsync("./api/OpportunityInfo/UpdateOppAssignment", asgn);
                    }

                    OppAssignmentlist = await http.GetFromJsonAsync<List<vwAssignment>>("./api/OpportunityInfo/LoadOppAssignments?Id=" + opp.OppID);
                    await JS.InvokeAsync<string>("LoadSimpleDataTable", "#tblAssignments");

                    IsPromotable = false;
                    RevisitDate = null;
                    ShowCloseOppModal = false;
                    StateHasChanged();
                }
            }

        }

        private void AddAssignment()
        {
            Assignment asgn = new()
            {
                AssignID = 0,
                AssignedObjectType = Constants.Opportunity,
                ObjectID = opp.OppID
            };

            AssignmentDialog.Show(asgn);
        }

        protected void EditAssignment(vwAssignment vwAsgn)
        {
            Assignment asgn = new()
            {
                AssignID = vwAsgn.AssignID,
                AssignedObjectType = vwAsgn.AssignedObjectType,
                ObjectID = vwAsgn.ObjectID,
                EmployeeID = vwAsgn.EmployeeID,
                FollowUp = vwAsgn.FollowUp,
                Responsibility = vwAsgn.Responsibility,
                IsPrimary = vwAsgn.IsPrimary,
                Role = vwAsgn.Role,
                StatusCode = vwAsgn.StatusCode
            };
            AssignmentDialog.Show(asgn);
        }

        public async void AssignmentDialog_OnDialogClose()
        {
            OppAssignmentlist = await http.GetFromJsonAsync<List<vwAssignment>>("./api/OpportunityInfo/LoadOppAssignments?Id=" + opp.OppID.ToString());
            await JS.InvokeAsync<string>("LoadSimpleDataTable", "#tblAssignments");
            StateHasChanged();
        }

        private void AddDiscussion()
        {
            Discussion dis = new()
            {
                DiscussionID = 0,
                AssignedObjectType = Constants.Opportunity,
                ObjectID = opp.OppID
            };

            DiscussionDialog.Show(dis);
        }

        protected void EditDiscussion(Discussion asgn)
        {
            DiscussionDialog.Show(asgn);
        }

        public async void DiscussionDialog_OnDialogClose()
        {
            OppDiscussionlist = await http.GetFromJsonAsync<List<Discussion>>("./api/OpportunityInfo/LoadOppDiscussions?Id=" + opp.OppID.ToString());
            await JS.InvokeAsync<string>("LoadSimpleDataTable", "#tblNotes");
            StateHasChanged();
        }

        public void OpenCustomerModal()
        {
            if (BillToPrompt == "Clear Bill-To")
            {
                IsPromotable = true;
                CustomerPrompt = "Prospect Name";
                BillToPrompt = "Select Bill-To";
                opp.CustomerID = "";
                opp.CustomerName = "";
                opp.CustomerCity = "";
                opp.CustomerState = "";
                opp.CustomerZip = "";
                opp.CustomerAddr1 = "";
                opp.CustomerAddr2 = "";
                opp.CustomerPhone = "";
            }
            else
            {
                ShowCustomerSearch = true;
            }
            
            StateHasChanged();
        }

        public void CloseCustomerModal()
        {
            SearchText = "";
            SearchedCustomersList = null;
            ShowCustomerSearch = false;
            StateHasChanged();
        }

        protected async Task BindCustomers()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                SearchedCustomersList = await http.GetFromJsonAsync<List<Customer>>("./api/OpportunityInfo/SearchCustomers?SearchText=" + SearchText);
                await JS.InvokeAsync<string>("LoadSimpleDataTable", "#datatable");
                StateHasChanged();
            }
        }

        protected async void SelectCustomer(Customer cust)
        {
            IsPromotable = false;
            CustomerPrompt = "Bill-To Name";
            BillToPrompt = "Clear Bill-To";
            opp.CustomerID = cust.CustomerID;
            opp.CustomerName = cust.CustomerName;
            opp.CustomerCity = cust.City;
            opp.CustomerState = cust.State;
            opp.CustomerZip = cust.Zip;
            opp.CustomerAddr1 = cust.Address1;
            opp.CustomerAddr2 = cust.Address2;
            opp.CustomerPhone = cust.Phone;
            SearchText = "";
            SearchedCustomersList = null;
            ShowCustomerSearch = false;

            await SaveData();
            StateHasChanged();
        }



    }
}
