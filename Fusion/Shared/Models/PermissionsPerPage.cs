using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class PermissionsPerPage
    {
        public int PermissionsPerPageID { get; set; }
        public int RoleID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int PageID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int PermissionID { get; set; }
        public DateTime? DateAdded { get; set; }
        public int? AddedByID { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string PageURL { get; set; }
        public string LastModifiedByID { get; set; }
    }
}
