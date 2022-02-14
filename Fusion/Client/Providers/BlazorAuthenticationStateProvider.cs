using Blazored.SessionStorage;
using Fusion.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fusion.Client.Providers
{
    public class BlazorAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService sessionStorage;
        private readonly NavigationManager navManager;
        public BlazorAuthenticationStateProvider(ISessionStorageService sessionStorageService, NavigationManager _navManager)
        {
            sessionStorage = sessionStorageService;
            navManager = _navManager;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity = await GetClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }
        public async Task MakeUserAsAuthenticated()
        {
            ClaimsIdentity identity = await GetClaimsIdentity();

            if (identity.AuthenticationType == "apiauth_type")
            {
                var user = new ClaimsPrincipal(identity);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            }
        }
        private async Task<ClaimsIdentity> GetClaimsIdentity()
        {
            ClaimsIdentity identity = new ClaimsIdentity();

            try
            {
                Employee emp = await sessionStorage.GetItemAsync<Employee>("Employee");

                if (emp != null)
                {
                    if (emp.EmployeeID > 0 && !string.IsNullOrEmpty(emp.Role))
                    {
                        identity = new ClaimsIdentity(new[] {
                                   new Claim(ClaimTypes.Name, emp.FirstName + " " + emp.LastName),
                                   new Claim(ClaimTypes.Email, Convert.ToString(emp.EmailAddress)),
                                   new Claim(ClaimTypes.Sid, Convert.ToString(emp.EmployeeID)),
                                   new Claim(ClaimTypes.Role, Convert.ToString(emp.Role)),
                                   new Claim("LoggedIn","true"),
                        }, "apiauth_type");
                    }

                    if (emp.EmployeeID > 0 && Convert.ToString(emp.Role).ToUpper() == "ADMIN")
                        identity.AddClaim(new Claim("AdminUser", "true"));

                    if (emp.EmployeeID > 0 && Convert.ToString(emp.Role).ToUpper() == "USER")
                        identity.AddClaim(new Claim("RegularUser", "true"));

                    if (emp.EmployeeID > 0 && Convert.ToString(emp.Role).ToUpper() == "SALES")
                        identity.AddClaim(new Claim("SalesUser", "true"));
                }
            }
            catch (Exception ex)
            {

            }
            
            return identity;
        }
        public void MarkUserAsLoggedOut()
        {
            sessionStorage.ClearAsync();

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

            navManager.NavigateTo("./", forceLoad: false);
        }
    }
}