using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public dynamic Result { get; set; }
    }
}
