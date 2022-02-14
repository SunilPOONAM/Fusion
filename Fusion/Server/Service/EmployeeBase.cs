using Fusion.Server.Service.imp;
using Fusion.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fusion.Server.Service
{
    public abstract class EmployeeBase
    {
        public abstract Employee Login(string username, string password);
        public abstract List<Employee> GetEmployees();
        public abstract Employee GetEmployeeById(int id);

    }
}
