using Fusion.Server.Service.imp;
using Fusion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fusion.Server.Service
{
    public abstract class CodesBase
    {
        public abstract List<tblCode> GetCodesByType(string key);

    }
}
