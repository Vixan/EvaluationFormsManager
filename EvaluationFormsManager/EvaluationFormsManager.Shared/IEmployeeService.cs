using IdentityServer.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvaluationFormsManager.Core.Shared
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(string currentUserId, string currentUrl);
    }
}
