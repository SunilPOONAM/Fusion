using Fusion.Client.Providers;
using Fusion.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;


namespace Fusion.Client.Pages
{
    public partial class Login : ComponentBase
    {
        HttpClient http = new HttpClient();
        private Employee employee = new();
        private string loader;
        public string errorMessage = string.Empty;

        [Inject]
        public AuthenticationStateProvider stateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            http.BaseAddress = new Uri(MyNavigationManager.Uri);
        }
        protected async Task HandleValidSubmit()
        {
            loader = "<div class='loader'><img src='./Content/images/Vp3R.gif'/></div>";
            try
            {
                ResponseModel res = await http.GetFromJsonAsync<ResponseModel>("./api/login?username=" + employee.EmailAddress + "&password=" + employee.UserID + "");
                if (!res.Status)
                {
                    errorMessage = res.Message;
                    loader = "";
                }
                else
                {
                    employee = JsonSerializer.Deserialize<Employee>(JsonSerializer.Serialize(res.Result), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    await sessionStorage.SetItemAsync("Employee", employee);
                    await ((BlazorAuthenticationStateProvider)stateProvider).MakeUserAsAuthenticated();
                    var state = await stateProvider.GetAuthenticationStateAsync();

                    if (state.User.HasClaim("LoggedIn", "true"))
                    {
                        MyNavigationManager.NavigateTo("./Dashboard");
                    }
                    else
                    {
                        errorMessage = "You do not have permission to enter this site.";
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            loader = "none";

        }
    }
}
