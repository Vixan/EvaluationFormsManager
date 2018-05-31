using EvaluationFormsManager.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer.Domain;

namespace EvaluationFormsManager.Core
{
    public class EmployeeService : IEmployeeService
    {
        public IEnumerable<Employee> GetEmployees(string currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
