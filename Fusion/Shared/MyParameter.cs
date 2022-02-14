using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Shared
{
    public class MyParameter
    {
        public string Name;
        public object Value;
        public MyParameter(string _Name, object _Value)
        {
            this.Name = _Name;
            this.Value = _Value;
        }
    }
}
