using Fusion.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Fusion.Client.Components
{
    public partial class AssignmentDialog : ComponentBase
    {
        HttpClient http = new HttpClient();
        public Assignment assignment { get; set; }
        public bool ShowDialog { get; set; }
        public virtual List<Employee> EmployeesList { get; set; }
        public virtual List<tblCode> AsgnstatusList { get; set; }
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        protected override async Task OnInitializedAsync()
        {
            http.BaseAddress = new Uri(MyNavigationManager.Uri);
            EmployeesList = await http.GetFromJsonAsync<List<Employee>>("./api/OpportunityInfo/EmployeesList");
            AsgnstatusList = await http.GetFromJsonAsync<List<tblCode>>("./api/OpportunityInfo/AsgnStatusList");
        }

        public void Show(Assignment asgn)
        {
            assignment = asgn;
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            HttpResponseMessage result = new HttpResponseMessage();

            var jsonData = JsonSerializer.Serialize(assignment);
            var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");

            if (assignment.AssignID > 0)
            {
                result = await http.PostAsync("./api/OpportunityInfo/UpdateOppAssignment", contentData);
            }
            else
            {
                result = await http.PostAsync("./api/OpportunityInfo/AddOppAssignment", contentData);
            }

            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(true);

        }
    }
}
