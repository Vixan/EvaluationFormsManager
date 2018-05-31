using EvaluationFormsManager.Core.Shared;
using IdentityServer.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EvaluationFormsManager.Core
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(string currentUserId, string currentUrl)
        {
            List<Employee> employees = new List<Employee>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(currentUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Employee/GetAllEmployees");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    employees = JsonConvert.DeserializeObject<List<Employee>>(result);
                }

                return employees;
            }
        }
    }
}
