using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class Page
    {
        public int PageID { get; set; }

        public string PageName { get; set; }

        public string PageURL { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? AddedByID { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public string LastModifiedByID { get; set; }
    }
}
