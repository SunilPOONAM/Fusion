using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Fusion.Shared;
using Fusion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fusion.Client.Pages
{
    public partial class PermissionsPage : ComponentBase
    {
        HttpClient http = new HttpClient();
        public List<Role> Roles { get; private set; }
        public List<Page> Pages { get; private set; }
        public List<Permission> permissions { get; private set; }

        private Employee employee = new Employee();
        private List<PermissionsPerPage> _permissionsPerPage;
        public string errorMessage;
        private string loader;

        public List<PermissionsPerPage> permissionsPerPage
        {
            get { return _permissionsPerPage; }
            set
            {
                _permissionsPerPage = value;
                Refresh();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            loader = "<div class='loader'><img src='./Content/images/Vp3R.gif'/></div>";
            http.BaseAddress = new Uri(MyNavigationManager.Uri);
            employee = await sessionStorage.GetItemAsync<Employee>("Employee");
            await LoadPermissionsData();
            loader = "";
        }
        protected async Task LoadPermissionsData()
        {
            await LoadRolesList();
            await LoadPageList();
            await LoadPermissionsList();
        }
        private async Task LoadRolesList()
        {
            ResponseModel res = await http.GetFromJsonAsync<ResponseModel>("./api/Permissions/GetRoles");
            if (!res.Status)
            {
                Roles = new List<Role>();
            }
            else
            {
                Roles = JsonSerializer.Deserialize<List<Role>>(JsonSerializer.Serialize(res.Result), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (Roles.Count > 0)
                {
                    SelectedRole = Roles[0].RoleID;
                    await LoadPermissionsPerPageList(SelectedRole);
                }
            }
        }

        private async Task LoadPageList()
        {
            ResponseModel res = await http.GetFromJsonAsync<ResponseModel>("./api/Permissions/GetPages");
            if (!res.Status)
            {
                Pages = new List<Page>();
            }
            else
            {
                Pages = JsonSerializer.Deserialize<List<Page>>(JsonSerializer.Serialize(res.Result), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private async Task LoadPermissionsList()
        {
            ResponseModel res = await http.GetFromJsonAsync<ResponseModel>("./api/Permissions/GetPermissions");
            if (!res.Status)
            {
                permissions = new List<Permission>();
            }
            else
            {
                permissions = JsonSerializer.Deserialize<List<Permission>>(JsonSerializer.Serialize(res.Result), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private async Task LoadPermissionsPerPageList(int RoleId)
        {
            ResponseModel res = await http.GetFromJsonAsync<ResponseModel>("./api/Permissions/GetPermissionsPerPage?RoleId=" + RoleId);
            if (!res.Status)
            {
                permissionsPerPage = new List<PermissionsPerPage>();
            }
            else
            {
                permissionsPerPage = JsonSerializer.Deserialize<List<PermissionsPerPage>>(JsonSerializer.Serialize(res.Result), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            Refresh();
        }

        private void LoadPageUrl(ChangeEventArgs e)
        {
            var SelectedString = Convert.ToInt32(e.Value);
            string url = Pages.Where(m => m.PageID == SelectedString).FirstOrDefault().PageURL;
            permissionsPerPage.LastOrDefault().PageURL = MyNavigationManager.BaseUri.Substring(0, MyNavigationManager.BaseUri.Length - 1) + url;

            Refresh();
        }
        public void Refresh()
        {
            StateHasChanged();
        }
        private int selectedRole;
        private int SelectedRole
        {
            get { return selectedRole; }
            set
            {
                selectedRole = value;
                LoadPermissionsPerPageList(value);
            }
        }
        private string selectedPage;
        private string SelectedPage
        {
            get { return selectedPage; }
            set
            {
                selectedPage = value;
                //LoadPageUrl();
            }
        }
        private string selectedPermission;
        private string SelectedPermission
        {
            get { return selectedPermission; }
            set
            {
                selectedPermission = value;
            }
        }
        private async Task HandleValidSubmit()
        {
            try
            {
                if (permissionsPerPage.Count > 0)
                {
                    var newRow = permissionsPerPage.Where(m => m.PermissionsPerPageID == 0).FirstOrDefault();
                    if (newRow != null)
                    {
                        if (newRow.PageID > 0 && newRow.PermissionID > 0)
                        {
                            newRow.AddedByID = employee.EmployeeID;
                            newRow.RoleID = selectedRole;

                            var jsonData = JsonSerializer.Serialize(newRow);
                            var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");
                            var res = await http.PostAsync("./api/Permissions/AddPermissionPerPage", contentData);

                            var response = await res.Content.ReadAsStringAsync();
                            var responseModel = JsonSerializer.Deserialize<ResponseModel>(response, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });

                            if (responseModel.Status)
                            {
                                var Result = JsonSerializer.Deserialize<int>(JsonSerializer.Serialize(responseModel.Result));
                                permissionsPerPage.LastOrDefault().PermissionsPerPageID = Result;
                            }
                            else
                            {
                                errorMessage = responseModel.Message;
                                return;
                            }
                        }
                        else
                        {
                            errorMessage = "PageID & PermissionID both must be selected";
                            return;
                        }
                    }
                }
                errorMessage = "";
                permissionsPerPage.Add(new PermissionsPerPage());
                Refresh();
            }
            catch (Exception ex)
            {
                errorMessage = "An error occured";
            }
        }

        private async Task DeletePermissionPerPage(int permissionPerPageId)
        {
            try
            {
                if (permissionsPerPage.Count > 0)
                {
                    var selectedRow = permissionsPerPage.Where(m => m.PermissionsPerPageID == permissionPerPageId).FirstOrDefault();
                    if (selectedRow != null)
                    {
                        if (permissionPerPageId > 0)
                        {
                            var res = await http.GetFromJsonAsync<ResponseModel>("./api/Permissions/DeletePermission?permissionPerPageId=" + permissionPerPageId);
                            if (!res.Status)
                            {
                                errorMessage = res.Message;
                                return;
                            }

                        }
                        errorMessage = "";
                        permissionsPerPage.Remove(selectedRow);
                        Refresh();
                    }
                }


            }
            catch (Exception ex)
            {
                errorMessage = "An error occured";
            }
        }
    }
}
