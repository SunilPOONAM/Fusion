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
    public partial class DiscussionDialog : ComponentBase
    {
        HttpClient http = new HttpClient();
        public Discussion discussion { get; set; }
        public bool ShowDialog { get; set; }
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        protected override async Task OnInitializedAsync()
        {
            http.BaseAddress = new Uri(MyNavigationManager.Uri);
        }

        public void Show(Discussion dis)
        {
            discussion = dis;
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

            var jsonData = JsonSerializer.Serialize(discussion);
            var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");
            
            if (discussion.DiscussionID > 0)
            {
                result = await http.PostAsync("./api/OpportunityInfo/UpdateOppDiscussion", contentData);
            }
            else
            {
                result = await http.PostAsync("./api/OpportunityInfo/AddOppDiscussion",contentData);
            }

            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(true);
            
        }
    }
}
