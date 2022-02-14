using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

namespace Fusion.Client.Pages
{
    
    public partial class Dashboard : ComponentBase
    {
        HttpClient http = new HttpClient();
        private Employee employee = new Employee();
        public List<vwAssignment> Assignments { get; private set; }
        private string loader;

        protected override async Task OnInitializedAsync()
        {
            loader = "<div class='loader'><img src='./Content/images/Vp3R.gif'/></div>";
            http.BaseAddress = new Uri(MyNavigationManager.Uri);
            employee = await sessionStorage.GetItemAsync<Employee>("Employee");
            await btnMyAssignments();
            loader = "";
        }

        protected async Task btnMyAssignments()
        {
            await LoadAssignmentList();
        }

        protected async Task btnTeamAssignemnts()
        {
            await LoadTeamAssignmentList();
        }

        protected void GoToAssignment(string objectType, int Id)
        {
            switch (objectType)
            {
                case "Opportunity":
                    MyNavigationManager.NavigateTo("./OpportunityInfo?Id=" + Id);
                    break;
                case "Agreement":
                    break;
            }


        }

        protected void NewOpportunity()
        {
            MyNavigationManager.NavigateTo("./OpportunityInfo?Id");
        }

        protected string GetPriorityColor(DateTime? followup)
        {
            var dt = followup ?? Convert.ToDateTime("1900-01-01");
            var today = DateTime.Now.Date;
            if (dt <= today)
            {
                return "red";
            }
            else if (today.AddDays(7) >= dt)
            {
                return "orange";
            }
            else
            {
                return "green";
            }
        }

        private async Task LoadAssignmentList()
        {
            loader = "<div class='loader'><img src='./Content/images/Vp3R.gif'/></div>";
            ResponseModel res = await http.GetFromJsonAsync<ResponseModel>("./api/Dashboard/GetAssignments?empID=" + employee.EmployeeID);
            if (!res.Status)
            {
                //errorMessage = res.Message;
                Assignments = new List<vwAssignment>();
                loader = "";
            }
            else
            {
                Assignments = JsonSerializer.Deserialize<List<vwAssignment>>(JsonSerializer.Serialize(res.Result), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                loader = "";
            }

            await JS.InvokeAsync<string>("LoadDataTable", "#myTable");

        }

        private async Task LoadTeamAssignmentList()
        {
            loader = "<div class='loader'><img src='./Content/images/Vp3R.gif'/></div>";
            ResponseModel res = await http.GetFromJsonAsync<ResponseModel>("./api/Dashboard/GetTeamAssignments?empID=" + employee.EmployeeID);
            if (!res.Status)
            {
                Assignments = new List<vwAssignment>();
                loader = "";
            }
            else
            {
                Assignments = JsonSerializer.Deserialize<List<vwAssignment>>(JsonSerializer.Serialize(res.Result), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                loader = "";
            }

            await JS.InvokeAsync<string>("LoadDataTable", "#myTable");
        }

    }
}
