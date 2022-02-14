using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fusion.Shared.Models;
using System.Data;
using Blazored.SessionStorage;
using Fusion.Server.Helper;
using Fusion.Server.Service;
using Fusion.Server.Service.imp;

namespace Fusion.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        EmployeeBase employeeBase = new ManageEmployee();
        // GET: api/<Login>
        [HttpGet]
        public ResponseModel Get(string username, string password)
        {
            ResponseModel res = new ResponseModel();
            Employee emp = employeeBase.Login(username, password);
            if (emp.EmployeeID > 0)
            {
                res.Result = emp;
                res.Status = true;
            }
            else
            {
                res.Message = "User not found";
                res.Status = false;
            }
            return res;


        }
    }
}
