using IdentityServer.Domain;
using System.Collections.Generic;

namespace EvaluationFormsManager.Core.Shared
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees(string currentUserId);
    }
}
