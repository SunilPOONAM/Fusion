using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared.Models
{
    public class tblCode
    {
        public string CodeType { get; set; }
        public string CodeID { get; set; }
        public string CodeDesc { get; set; }
        public string CodeMemo { get; set; }
        public Nullable<short> CodeNum { get; set; }
        public byte[] upsize_ts { get; set; }
    }
}
