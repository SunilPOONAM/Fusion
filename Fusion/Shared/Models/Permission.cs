using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class Permission
    {
        public int PermissionsID { get; set; }

        public string PermissionName { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? AddedByID { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string LastModifiedByID { get; set; }
    }
}
