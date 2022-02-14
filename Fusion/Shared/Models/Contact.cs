using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public  class Contact
    {
        public int ContactID { get; set; }
        public string CustomerID { get; set; }
        [Required(ErrorMessage= "Please enter student name.")]
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string ContactType { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ContactComment { get; set; }
        public bool Inactive { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
