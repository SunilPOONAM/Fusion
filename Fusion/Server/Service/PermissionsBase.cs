using Fusion.Server.Service.imp;
using Fusion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fusion.Server.Service
{
    public abstract class PermissionsBase
    {
        public abstract List<Role> GetRoleInfo();
        public abstract List<Page> GetPageInfo();
        public abstract List<Permission> GetPermissionInfo();
        public abstract List<PermissionsPerPage> GetPermissionsPerPage(int RoleId);

        public abstract ResponseModel AddPermissionPerPage(PermissionsPerPage model);

        public abstract bool DeletePermissionPerPage(int permissionPerPageId);
    }
}
