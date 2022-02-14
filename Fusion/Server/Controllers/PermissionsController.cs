using Microsoft.AspNetCore.Mvc;
using Fusion.Server.Service.imp;
using Fusion.Server.Service;
using Fusion.Shared.Models;
using Fusion.Shared;
using System.Collections.Generic;
using System;

namespace Fusion.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        PermissionsBase mnper = new ManagePermissions();
        [HttpGet]
        public ResponseModel GetRoles()
        {
            ResponseModel res = new ResponseModel();
            List<Role> lst = new List<Role>();
            lst = mnper.GetRoleInfo();
            if (lst.Count > 0)
            {
                res.Status = true;
                res.Result = lst;
            }
            else
                res.Status = false;
            return res;
        }

        public ResponseModel GetPages()
        {
            ResponseModel res = new ResponseModel();
            List<Page> lst = new List<Page>();
            lst = mnper.GetPageInfo();
            if (lst.Count > 0)
            {
                res.Status = true;
                res.Result = lst;
            }
            else
                res.Status = false;
            return res;
        }

        public ResponseModel GetPermissions()
        {
            ResponseModel res = new ResponseModel();
            List<Permission> lst = new List<Permission>();
            lst = mnper.GetPermissionInfo();
            if (lst.Count > 0)
            {
                res.Status = true;
                res.Result = lst;
            }
            else
                res.Status = false;
            return res;
        }

        public ResponseModel GetPermissionsPerPage(int RoleId)
        {
            ResponseModel res = new ResponseModel();
            List<PermissionsPerPage> lst = new List<PermissionsPerPage>();
            lst = mnper.GetPermissionsPerPage(RoleId);
            if (lst.Count > 0)
            {
                res.Status = true;
                res.Result = lst;
            }
            else
                res.Status = false;
            return res;
        }

        public ResponseModel AddPermissionPerPage([FromBody] PermissionsPerPage model)
        {
            ResponseModel res = new ResponseModel();
            res = mnper.AddPermissionPerPage(model);
            return res;
        }

        public ResponseModel DeletePermission(int permissionPerPageId)
        {
            ResponseModel res = new ResponseModel();

            bool result = mnper.DeletePermissionPerPage(permissionPerPageId);
            res.Status = result;
            return res;
        }
    }
}
